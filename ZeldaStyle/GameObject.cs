using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaStyle
{
    public sealed class GameObject
    {
        public int ID { get; set; }

        private readonly List<Component> components;

        public GameObject() {
            components = new List<Component>();
        }

        public void RemoveComponent(Component component) {
            components.Remove(component);
        }

        public Component AddComponent(Component component) {
            components.Add(component);
            component.Initialize(this);
            return component;
        }

        public void Update(float deltaTime) {
            //Console.WriteLine("GameObject Update called! deltaTime = " + deltaTime);
            foreach (Component comp in components)
            {
                comp.Update(deltaTime);
            }
        }

        public void StartUpdate(float deltaTime) {
            foreach (Component comp in components) {
                comp.StartUpdate(deltaTime);
            }
        }

        public void EndUpdate(float deltaTime) {
            foreach (Component comp in components) {
                comp.LastUpdate(deltaTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < components.Count; i++) {
                components[i].PreDraw(spriteBatch);
            }
            for (int i = 0; i < components.Count; i++) {
                components[i].Draw(spriteBatch);
            }
            for (int i = 0; i < components.Count; i++) {
                components[i].PostDraw(spriteBatch);
            }            
        }

        public TComponentType GetComponent<TComponentType>() where TComponentType : Component {
            return components.OfType<TComponentType>().First();            
        }

    }
}
