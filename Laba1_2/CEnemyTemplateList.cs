using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Laba1_2
{
    public class CEnemyTemplateList
    {
        private List<CEnemyTemplate> enemies;

        public CEnemyTemplateList()
        {
            enemies = new List<CEnemyTemplate>();
        }

        public void AddEnemy(CEnemyTemplate enemy)
        {
            enemies.Add(enemy);
        }

        public void RemoveEnemy(CEnemyTemplate enemy)
        {
            enemies.Remove(enemy);
        }

        public List<CEnemyTemplate> GetEnemies()
        {
            return enemies;
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(enemies, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка сохранения: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath)
        {
            try
            {
                string jsonFromFile = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var loadedEnemies = JsonSerializer.Deserialize<List<CEnemyTemplate>>(jsonFromFile, options);
                enemies.Clear();
                enemies.AddRange(loadedEnemies);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка загрузки: {ex.Message}");
            }
        }

    }
}
