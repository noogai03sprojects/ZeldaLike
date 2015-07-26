using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaStyle
{
    public abstract class Component
    {
        private GameObject Owner;

        public virtual void Initialize(GameObject _object) {
            Owner = _object;
        }

        public int GetOwnerID() {
            return Owner.ID;
        }

        public void RemoveMe() {
            Owner.RemoveComponent(this);
        }

        public TComponentType GetComponent<TComponentType>() where TComponentType : Component {
            return Owner.GetComponent<TComponentType>();
        }

        #region Update

        public virtual void StartUpdate(float deltaTime) {

        }

        public virtual void Update(float deltaTime) {

        }

        public virtual void LastUpdate(float deltaTime) {

        }

        #endregion

        #region Draw

        public virtual void PreDraw(SpriteBatch spriteBatch) {

        }

        public virtual void Draw(SpriteBatch spriteBatch) {

        }

        public virtual void PostDraw(SpriteBatch spriteBatch) {

        }

        #endregion

        public virtual void Awake() {

        }

        
    }
}
