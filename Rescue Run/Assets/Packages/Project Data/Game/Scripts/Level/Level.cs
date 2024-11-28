using System;
using UnityEngine;

namespace Watermelon
{
    public class Level : MonoBehaviour
    {
        public event Action<PlayerBehavior> OnLevelCreated;

        [SerializeField] Transform spawnPoint;

        [SerializeField] LevelTutorialBehaviour levelTutorialBehaviour;

        [SerializeField]
        private PlayerProgress playerProgress;

        [SerializeField] Zone[] zones;
        public Zone[] Zones => zones;

        public void Start()
        {
            LevelController.OnLevelCreated(this);
        }

        public void OnLevelLoaded(LevelSave levelSave)
        {
            // Check if save is exists (should be null on first launch)
            if (levelSave != null && !levelSave.ZoneSaves.IsNullOrEmpty())
            {
                for (int i = 0; i < levelSave.ZoneSaves.Length; i++)
                {
                    for (int j = 0; j < zones.Length; j++)
                    {
                        if (zones[j].ID == levelSave.ZoneSaves[i].ID)
                        {
                            zones[j].Load(levelSave.ZoneSaves[i]);

                            break;
                        }
                    }
                }
            }

            levelSave.LinkZones(zones);
        }

        public void InitialisePlayer(PlayerBehavior playerBehavior)
        {
            for (int i = 0; i < zones.Length; i++)
            {
                zones[i].Initialise(playerBehavior);
            }

            if (playerProgress != null)
                playerProgress.InitialisePlayer(playerBehavior);

            OnLevelCreated?.Invoke(playerBehavior);
        }

        public void InitialiseScenePersistenceManager(ScenePersistenceManager scenePersistenceManager)
        {
            if (playerProgress != null)
                playerProgress.InitialiseScenePersistenceManager(scenePersistenceManager);
        }

        public void OnGameLoaded()
        {
            if (levelTutorialBehaviour != null)
            {
                levelTutorialBehaviour.OnGameLoaded();
            }
        }

        public void RecalculateNurses()
        {
            for (int i = 0; i < zones.Length; i++)
            {
                zones[i].RecalculateNurses();
            }
        }

        public void Unload()
        {
            for (int i = 0; i < zones.Length; i++)
            {
                zones[i].AnimalSpawner.DisableAutoSpawn();
            }
        }

        public Vector3 GetSpawnPoint()
        {
            return spawnPoint.position;
        }

        [Button("Pick Zones")]
        public void PickZones()
        {
            zones = GetComponentsInChildren<Zone>();
            for (int i = 0; i < zones.Length; i++)
            {
                zones[i].ID = i;

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(zones[i]);
#endif
            }
        }
    }
}