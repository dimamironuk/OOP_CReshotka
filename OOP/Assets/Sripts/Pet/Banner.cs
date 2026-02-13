using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Banner : MonoBehaviour
{
    [SerializeField] private List<Sprite> rarePets;
    [SerializeField] private List<Sprite> epicPets;
    [SerializeField] private List<Sprite> legendaryPets;
    [SerializeField] private GameObject effectPet;
    [SerializeField] private Image spritePet; 
    const int garantLegendary = 60;
    const int garantEpic = 10;
    [SerializeField] private int totalRolls;
    [SerializeField] private int epicRolls;
    [SerializeField] private int countRitual;
    [SerializeField] private TextMeshProUGUI textRitual;
    private void Awake()
    {
        totalRolls = PlayerPrefs.GetInt("totalRolls");
        epicRolls = PlayerPrefs.GetInt("epicRolls");
        countRitual = PlayerPrefs.GetInt("countRitual");
        textRitual.text = countRitual.ToString();
        countRitual = 10;
    }

    public void RitualPet()
    {
        if (countRitual == 0)
        {
            Debug.Log("No money((((");
            return;
        }

        GameObject petGO = new GameObject("Pet");
        PetController pet = petGO.AddComponent<PetController>();
        pet.sprite = petGO.AddComponent<SpriteRenderer>();

        if (totalRolls >= garantLegendary)
        {
            pet.sprite.sprite = legendaryPets[Random.Range(0, legendaryPets.Count)];
            totalRolls = 0;
            epicRolls = 0;
            pet.rarity = PetController.Rarity.Legendary;
        }
        else if (epicRolls >= garantEpic)
        {
            pet.sprite.sprite = epicPets[Random.Range(0, epicPets.Count)];
            epicRolls = 0;
            pet.rarity = PetController.Rarity.Epic;
        }
        else
        {
            float roll = Random.Range(0f, 1f);

            if (roll < 0.05f)
            {
                pet.sprite.sprite = legendaryPets[Random.Range(0, legendaryPets.Count)];
                totalRolls = 0;
                epicRolls = 0;
                pet.rarity = PetController.Rarity.Legendary;
            }
            else if (roll < 0.20f)
            {
                pet.sprite.sprite = epicPets[Random.Range(0, epicPets.Count)];
                epicRolls++;
                pet.rarity = PetController.Rarity.Epic;
            }
            else
            {
                pet.sprite.sprite = rarePets[Random.Range(0, rarePets.Count)];
                epicRolls++;
                pet.rarity = PetController.Rarity.Rare;
            }
        }
        Debug.Log(pet.rarity);
        spritePet.sprite = pet.sprite.sprite;
        effectPet.SetActive(true);
        totalRolls++;
        countRitual--;
        textRitual.text = countRitual.ToString();

    }
}
