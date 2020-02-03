using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyRim.Models
{
    /// <summary>
    /// Це інтерфейс основного о'бєкту в грі.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        ///     Метод підгрузки ресурсів об'єкту.
        /// </summary>
        /// <param name="container">Об'єкт, що займається загрузкою ресурсів.</param>
        void LoadContent(Controllers.Container container);
        /// <summary>
        ///     Метод обробки логіки об'єкту.
        /// </summary>
        /// <param name="gameTime">Час моменту гри на момент виконання методу.</param>і
        void Update(GameTime gameTime);
    }
    /// <summary>
    /// Інтерфейс об'єктів, що можуть динамічно змінювати свої положення/розміри/швидкість...
    /// </summary>
    public interface IPhysicalObject:IGameObject, IUpdateable
    {
        /// <summary>
        ///     Позиція об'єкту в просторі.
        /// </summary>
        Vector2 Position { get; set; }
        /// <summary>
        ///     Кут повороту об'єкту відносно додатнього напрямку осі X.
        /// </summary>
        float Angle { get; set; }
        /// <summary>
        ///     Швидкість обертання об'єкту.
        ///     Додатні значення - за годинниковою стрілкою.
        ///     Від'ємні значення - проти годинникової стрілки.
        /// </summary>
        float AngularVelocity { get; set; }
        /// <summary>
        /// Вектор швидкості та напрямку об'єкта.
        /// </summary>
        Vector2 Velocity { get; set; }
        /// <summary>
        ///     Метод обробки фізики об'єкту.
        /// </summary>
        /// <param name="gameTime">Час моменту гри на момент виконання методу.</param>і
        void UpdatePhysic(GameTime gameTime);
    }
    /// <summary>
    /// Інтерфейс об'єктів що мають змогу відображатися на екрані гри
    /// </summary>
    public interface IDrawableObject :IDrawable,IGameObject
    { 
        /// <summary>
        ///     Видимий кусок текстури.
        /// </summary>
        Rectangle SourceRectangle { get; }
        /// <summary>
        ///     Центр об'єкту.
        /// </summary>
        Vector2 Origin { get; }
        /// <summary>
        ///     Множник розміру об'єкту.
        /// </summary>
        Vector2 Scale { get; }
        /// <summary>
        ///     Швидкість зміни розміру об'єкту.
        /// </summary>
        Vector2 ScaleVelocity { get; set; }
        /// <summary>
        ///     Колір текстури.
        /// </summary>
        Vector4 Color { get; }
        /// <summary>
        ///     Головна текстура об'єкту.
        /// </summary>
        Texture2D Texture { get; }
        /// <summary>
        ///     Метод, що викликається коли об'єкт повинен себе намалювати
        /// </summary>
        /// <param name="spriteBatch">Дозволяє малювати групу спрайтів, використовуючи ті самі налаштування.</param>
        /// <param name="gameTime">Час моменту гри на момент виконання методу.</param>
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
    /// <summary>
    /// Інтерфейс об'єктів що можуть перетинатися між собою
    /// </summary>
    public interface IColidebleObject :IPhysicalObject,IUpdateable, IGameObject
    { 
        ///// <summary>
        /////     Метод, що викликається коли об'єкт повинен себе намалювати
        ///// </summary>
        ///// <param name="spriteBatch"></param>
        ///// <param name="gameTime"></param>
        //void Draw(SpriteBatch spriteBatch, GameTime gameTime);


        /// <summary>
        /// Метод провірки перетину цього проекту з іншим.
        /// </summary>
        /// <param name="other">Інший об'єкт з яким порівнюється даний.</param>
        /// <returns> Повертає результат </returns>
        bool IsColided(IColidebleObject other);
    }
}
