using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaxyRim.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyRim.Models.Particles
{
    /// <summary>
    /// Основний та загальний клас частинки в грі
    /// </summary>
    public class Particle : IParticle
    {
        /// <summary>
        /// Область текстури, що малюється.
        /// </summary>
        private Rectangle? sourcerectangle;
        /// <summary>
        /// Івент  наявності змін об'єкту.
        /// </summary>
        public event EventHandler<EventArgs> EnabledChanged;
        /// <summary>
        /// Івент оновлення порядку черги об'єкта при виклиці Update.
        /// </summary>
        public event EventHandler<EventArgs> UpdateOrderChanged;
        /// <summary>
        /// Івент зміни видимості об'єкту.
        /// </summary>
        public event EventHandler<EventArgs> VisibleChanged;
        /// <summary>
        /// Івент зміни порядку в черзі на відображення об'єкту.
        /// </summary>
        public event EventHandler<EventArgs> DrawOrderChanged;
        /// <inheritdoc/>
        public Rectangle SourceRectangle { get => sourcerectangle.Value; set => sourcerectangle = value; }
        /// <inheritdoc/>
        public Texture2D Texture { get; set; }              public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }               public float Angle { get; set; }
        public float AngularVelocity { get; set; }          public Vector4 Color { get; set; }
        public Vector2 Size { get; set; }                   public Vector2 SizeVel { get; set; }
        public Vector4 AlphaVel { get; set; }               public uint TTL { get; set; }
        public Vector2 Origin { get; set; }

        public float Speed { get; set; }

        public bool Enabled { get; set; }

        public int UpdateOrder { get; set; }

        public Vector2 Scale { get; set; }

        public bool Visible { get; set; }

        public int DrawOrder { get; set; }

        public Vector2 ScaleVelocity { get; set; }

        /// <summary>
        ///     Конструктор. Створює об'єкт Частинки
        /// </summary>
        /// <param name="texture">Текстура конкретної частинки.</param>
        /// <param name="position">Позиція в просторі конкретної частинки.</param>
        /// <param name="velocity">Текстура конкретної частинки.</param>
        /// <param name="angle">Кут повороту частинки.</param>
        /// <param name="angularVelocity">Швидкість обертання частинки.</param>
        /// <param name="color">Колір забарвлення текстури (Білий цілковито стає цим кольором).</param>
        /// <param name="size">Розмір. В скільки разів буде змінений розмір частинки.</param>
        /// <param name="ttl">Таймер життя частинки (в тіках гри)</param>
        /// <param name="sizeVel">Швидкість зміни розміру частинки</param>
        /// <param name="alphaVel">Швидкість зміни прозорості частинки</param>
        /// <param name="sourceRectangle">(Опціонально) Частина текстурки, що відображатиметься</param>
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Vector4 color, float size, UInt32 ttl,
            float sizeVel, float alphaVel, Rectangle? sourceRectangle)
        {
            Texture = texture;              Position = position;            Velocity = velocity;
            Angle = angle;                  Color = color;                  AngularVelocity = angularVelocity;
            Size = new Vector2(size);       SizeVel = new Vector2(sizeVel); AlphaVel = new Vector4(0, 0, 0, alphaVel);
            TTL = ttl;                      sourcerectangle = sourceRectangle;
            if (sourcerectangle.HasValue)   Origin = new Vector2(SourceRectangle.Width / 2f, SourceRectangle.Height / 2f);
            else                            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }
        /// <summary>
        ///     Конструктор. Створює об'єкт Частинки
        /// </summary>
        /// <param name="texture">Текстура конкретної частинки.</param>
        /// <param name="position">Позиція в просторі конкретної частинки.</param>
        /// <param name="velocity">Текстура конкретної частинки.</param>
        /// <param name="angle">Кут повороту частинки.</param>
        /// <param name="angularVelocity">Швидкість обертання частинки.</param>
        /// <param name="color">Колір забарвлення текстури (Білий цілковито стає цим кольором).</param>
        /// <param name="size">Розмір. В скільки разів буде змінений розмір частинки.</param>
        /// <param name="ttl">Таймер життя частинки (в тіках гри)</param>
        /// <param name="sizeVel">Швидкість зміни розміру частинки</param>
        /// <param name="alphaVel">Швидкість зміни прозорості частинки</param>
        /// <param name="sourceRectangle">(Опціонально) Частина текстурки, що відображатиметься</param>
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Vector4 color, Vector2 size, UInt32 ttl,
            Vector2 sizeVel, float alphaVel, Rectangle? sourceRectangle) 
        {
            Texture = texture;              Position = position;            Velocity = velocity;
            Angle = angle;                  Color = color;                  AngularVelocity = angularVelocity;
            Size = size;                    SizeVel = sizeVel;              AlphaVel = new Vector4(0, 0, 0, alphaVel);
            TTL = ttl;                      sourcerectangle = sourceRectangle;
            if (sourcerectangle.HasValue)   Origin = new Vector2(SourceRectangle.Width / 2f, SourceRectangle.Height / 2f);
            else                            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }


        /// <summary>
        ///     Метод обробки фізики частинки
        /// </summary>
        /// <param name="gameTime">Час моменту гри на момент виконання методу.</param>
        public void Update(GameTime gameTime)
        {
            TTL--;            
            Color -= AlphaVel;
            UpdatePhysic(gameTime);
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, sourcerectangle, new Color(Color),
                Angle, Origin, Size, SpriteEffects.None, 0);    //Мацькаєм
        }

        public virtual void ResetState()
        {

        }
        ///<seealso cref="IPhysicalObject.UpdatePhysic(GameTime)"/>
        public void UpdatePhysic(GameTime gameTime)
        {
            Position += Velocity;
            Angle += AngularVelocity; Size += SizeVel;
        }

        ///<inheritdoc/>
        public void Draw(GameTime gameTime)
        {
            IGameObject @object = new Particle(null, Vector2.Zero, Vector2.Zero, 0, 0, Vector4.Zero, 0, 0, 0, 0, null);
            //(@object as Particle).
        }
        

        public void LoadContent(Container container)
        {
            throw new NotImplementedException();
        }
    }
}
