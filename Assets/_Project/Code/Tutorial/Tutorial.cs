using System.Collections;
using _Project.Code.Core.Buyables;
using _Project.Code.Core.Items.Inventory;
using TMPro;
using UnityEngine;

namespace _Project.Code.Tutorial
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Collider2D _tutorialCollider;
        
        [SerializeField] private TMP_Text _tutorialText;
        [SerializeField] private ItemInventory _tutorialInventory;
        [SerializeField] private Joystick _joystick;

        [SerializeField] private Transform _arrow;
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        
        [SerializeField] private Buyable _buyable;

        private void Start()
        {
            _tutorialText.gameObject.SetActive(true);
            _tutorialCollider.enabled = true;
            _arrow.gameObject.SetActive(false);
            _tutorialInventory.gameObject.SetActive(false);
            StartCoroutine(TutorialCoroutine());
        }
        
        private IEnumerator TutorialCoroutine()
        {
            yield return new WaitForJoystickMove(_joystick);
            _tutorialText.gameObject.SetActive(false);
            
            _tutorialInventory.gameObject.SetActive(true);
            _arrow.gameObject.SetActive(true);
            
            _arrow.transform.position = _point1.position;

            yield return new WaitForSeconds(1f);
            
            while (true)
            {
                if (_tutorialInventory.IsEmpty)
                {
                    _arrow.transform.position = _point2.position;
                    break;
                }

                yield return null;
            }
            
            while (true)
            {
                if (_buyable.IsBought)
                {
                    Debug.Log("Buyed");
                    _arrow.gameObject.SetActive(false);
                    break;
                }

                yield return null;
            }
            
            _tutorialCollider.enabled = false;
        }
    }
}