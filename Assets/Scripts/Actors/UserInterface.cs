using UnityEngine;
using UnityEngine.UI;

namespace LumberjackVsMonsters
{
    public sealed class UserInterface : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private RawImage bloodImage;
        [SerializeField] private GameObject openDoor;
        [SerializeField] private GameObject pressE;
        [SerializeField] private Text gameOverText;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Text zoneText;
        [SerializeField] private GameObject zonePanel;
        private Player _player;
        private House _house;
        private DialogBehavior _dialog;
        private DoorBehavior _door;
        private ZoneBehavior _zone;
        private int _score;
        private bool _isGameOver;
        private bool _isDoorOpened;
        
        public void Initialize()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            _house = GameObject.Find("House").GetComponent<House>();
            _dialog = _house.GetComponent<DialogBehavior>();
            _door = _house.GetComponentInChildren<DoorBehavior>();
            _zone = GameObject.Find("Terrain").GetComponentInChildren<ZoneBehavior>();
            
            _player.MonsterHit += OnMonsterHit;
            _player.PlayerDied += OnPlayerDied;
            _house.HouseDestroyed += OnHouseDestroyed;
            _dialog.DialogEnded += OnDialogEnded;
            _door.DoorOpened += OnDoorOpened;
            _zone.PlayerEscaped += OnPlayerEscaped;
            _zone.PlayerInside += OnPlayerInside;
            _zone.PlayerOutside += OnPlayerOutside;
            
            openDoor.SetActive(false);
            gameOverPanel.SetActive(false);
            zonePanel.SetActive(false);
            pressE.SetActive(false);
        }

        private void OnDestroy()
        {
            _player.MonsterHit -= OnMonsterHit;
            _player.PlayerDied -= OnPlayerDied;
            _house.HouseDestroyed -= OnHouseDestroyed;
            _dialog.DialogEnded -= OnDialogEnded;
            _door.DoorOpened -= OnDoorOpened;
            _zone.PlayerEscaped -= OnPlayerEscaped;
            _zone.PlayerInside -= OnPlayerInside;
            _zone.PlayerOutside -= OnPlayerOutside;
        }
        
        private void OnPlayerOutside()
        {
            zonePanel.SetActive(true);
        }

        private void OnPlayerInside()
        {
            zonePanel.SetActive(false);
        }

        private void OnPlayerEscaped()
        {
            if (_isGameOver) return;
            
            GameOver();
        }

        private void OnDoorOpened()
        {
            _isDoorOpened = true;
            pressE.SetActive(false);
            openDoor.SetActive(false);
        }

        private void OnDialogEnded()
        {
            if (!_isDoorOpened)
                openDoor.SetActive(true);
        }

        private void OnHouseDestroyed()
        {
            if (_isGameOver) return;
            
            GameOver();

            gameOverText.text = $"Monsters have reached your family\nYou killed {_score} monsters";
        }

        private void OnPlayerDied()
        {
            if (_isGameOver) return;
            
            GameOver();

            gameOverText.text = $"You died\nYou killed {_score} monsters";
        }

        private void GameOver()
        {
            _isGameOver = true;
            
            gameOverPanel.SetActive(true);
        }

        private void OnMonsterHit()
        {
            _score++;
        }

        public void Tick()
        {
            if (!_isDoorOpened)
                pressE.SetActive(_door.IsNear);
            
            if (_isGameOver)
            {
                if (Input.GetButtonDown("Interact"))
                    Application.LoadLevel(Application.loadedLevel);
                
                return;
            }
            
            if (_zone.IsOutside && !_isGameOver)
            {
                zoneText.text = $"{_zone.Timer:F3}";
            }
            
            var color = bloodImage.color;
            color.a = 1 - _player.Health / _player.MaxHealth;

            bloodImage.color = color;
        }
    }
}