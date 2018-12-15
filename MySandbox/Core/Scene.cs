using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MySandbox.Core
{
    public class Scene
    {
        public static int[,] collision;
        public List<GameObject> gameObjects = new List<GameObject>();

        public string path = "Content/Maps/";
        public string file_name = "Scene.json";

        public string GetFullPath()
        {
            return Path.Combine(path, file_name);
        }

        public int Width, Height;

        public Scene(string path)
        {
            this.path = path;
            Load();
        }

        public Scene()
        {

        }

        public void Draw()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw();
            }
        }

        public void Load()
        {
            if (!File.Exists(GetFullPath()))
                return;

            using (StreamReader file = File.OpenText(GetFullPath()))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.TypeNameHandling = TypeNameHandling.Auto;
                serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

                gameObjects = (List<GameObject>)serializer.Deserialize(file, typeof(List<GameObject>));
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].UpdateTexture();
            }
        }

        public void Save()
        {
            if (gameObjects == null)
                return;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (StreamWriter file = File.CreateText(GetFullPath()))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.TypeNameHandling = TypeNameHandling.Auto;
                serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
                serializer.Serialize(file, gameObjects);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(gameTime);
            }
        }
    }
}
