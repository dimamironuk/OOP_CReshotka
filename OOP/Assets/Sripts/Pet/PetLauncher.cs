using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetLauncher : MonoBehaviour
{
    [SerializeField] private List<Sprite> allPets;
    [SerializeField] private List<RuntimeAnimatorController> allPetsAnimController;

    public static List<GameObject> _pets = new List<GameObject>(); // Ініціалізація відразу

    [SerializeField] private GameObject _pet; // Поточний активний пет на сцені
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _namePet;
    [SerializeField] private int chooseIndexPet = 0;
    [SerializeField] private SaveFileJSON saveSystem;

    private void Awake()
    {
        if (_pets == null) _pets = new List<GameObject>();
        LoadPets();
    }

    public void SavePets()
    {
        DataFilePet data = new DataFilePet();
        data.pets = new List<PetFile>();

        foreach (GameObject petGO in _pets)
        {
            if (petGO == null) continue;
            PetController pet = petGO.GetComponent<PetController>();

            data.pets.Add(new PetFile
            {
                namePet = pet.namePet,
                rarity = (int)pet.rarity
            });
        }

        if (_pet != null)
        {
            PetController petActive = _pet.GetComponent<PetController>();
            data.activePet = new PetFile
            {
                namePet = petActive.namePet,
                rarity = (int)petActive.rarity
            };
        }
        else if (_pets.Count > 0)
        {
            PetController firstPet = _pets[0].GetComponent<PetController>();
            data.activePet = new PetFile { namePet = firstPet.namePet, rarity = (int)firstPet.rarity };
        }

        saveSystem.SaveFile(data);
    }

    public void LoadPets()
    {
        DataFilePet data = saveSystem.Load();
        if (data == null || data.pets == null) return;

        foreach (var existingPet in _pets)
        {
            if (existingPet != null) Destroy(existingPet);
        }
        _pets.Clear();

        foreach (PetFile petFile in data.pets)
        {
            GameObject petGO = new GameObject("Pet_" + petFile.namePet);
            PetController pet = petGO.AddComponent<PetController>();
            pet.sprite = petGO.AddComponent<SpriteRenderer>();
            Animator anim = petGO.AddComponent<Animator>();

            pet.namePet = petFile.namePet;
            pet.rarity = (PetController.Rarity)petFile.rarity;
            pet.sprite.sprite = GetSpriteByName(petFile.namePet);

            pet.animController = GetAnimControllerByName(petFile.namePet);
            anim.runtimeAnimatorController = pet.animController;

            petGO.SetActive(false);
            DontDestroyOnLoad(petGO);

            _pets.Add(petGO);
        }

        if (data.activePet != null)
        {
            for (int i = 0; i < _pets.Count; i++)
            {
                PetController pc = _pets[i].GetComponent<PetController>();
                if (pc.namePet == data.activePet.namePet)
                {
                    chooseIndexPet = i;
                    ChoosePet();
                    UpdateUI(); 
                    break;
                }
            }
        }
        else if (_pets.Count > 0)
        {
            chooseIndexPet = 0;
            UpdateUI();
        }
    }

    public void ChoosePet()
    {
        if (_pets.Count == 0) return;
        if (chooseIndexPet >= _pets.Count) chooseIndexPet = 0;

        if (_pet != null)
        {
            Destroy(_pet);
        }

        if (chooseIndexPet < _pets.Count)
        {
            GameObject template = _pets[chooseIndexPet];

            Vector3 spawnPos = Vector3.zero;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) spawnPos = player.transform.position;

            _pet = Instantiate(template, spawnPos, Quaternion.identity);

            PetController pc = _pet.GetComponent<PetController>();
            pc.animController = allPetsAnimController[chooseIndexPet]; // Страховка

            _pet.SetActive(true);
            _pet.GetComponent<SpriteRenderer>().enabled = true;

            SavePets();
        }
    }

    private Sprite GetSpriteByName(string petName)
    {
        foreach (Sprite sprite in allPets)
        {
            if (sprite.name == petName) return sprite;
        }
        return null;
    }

    public RuntimeAnimatorController GetAnimControllerByName(string petName)
    {
        for (int i = 0; i < allPets.Count; i++)
        {
            if (allPets[i].name == petName)
            {
                if (i < allPetsAnimController.Count)
                    return allPetsAnimController[i];
            }
        }
        return null;
    }

    public bool IsRepetition(PetController pet)
    {
        foreach (GameObject p in _pets)
        {
            PetController pc = p.GetComponent<PetController>();
            if (pc.namePet == pet.namePet && pc.rarity == pet.rarity) // Порівнюємо за іменем, це надійніше
            {
                return true;
            }
        }
        return false;
    }

    public void AddNewPet(GameObject newPet)
    {
        _pets.Add(newPet);
        SavePets();
    }

    public void NextRight()
    {
        if (_pets.Count == 0) return;

        chooseIndexPet++;
        if (chooseIndexPet > _pets.Count - 1)
        {
            chooseIndexPet = 0;
        }
        UpdateUI();
    }

    public void NextLeft()
    {
        if (_pets.Count == 0) return;

        chooseIndexPet--;
        if (chooseIndexPet < 0)
        {
            chooseIndexPet = _pets.Count - 1;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(!_image || !_namePet) return;
        if (_pets.Count > 0 && chooseIndexPet < _pets.Count)
        {
            PetController pc = _pets[chooseIndexPet].GetComponent<PetController>();
            _image.sprite = pc.sprite.sprite;
            _namePet.text = pc.namePet;
        }
    }
}