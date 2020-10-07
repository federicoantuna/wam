using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WAM.Domain.Enums
{
    /// <summary>
    /// Game version flavor options.
    /// </summary>
    public class GameVersionFlavor
    {
        private static readonly IReadOnlyDictionary<GameVersionFlavorEnum, GameVersionFlavor> _gameVersionFlavorMap;

        private readonly GameVersionFlavorEnum _gameVersionFlavorEnum;
        private readonly String _name;
        private readonly String _curseforgeCode;

        /// <summary>
        /// Option for the Retail flavor of the game.
        /// </summary>
        public static readonly GameVersionFlavor Retail = new GameVersionFlavor(GameVersionFlavorEnum.Retail, "wow_retail");

        /// <summary>
        /// Option for the Classic flavor of the game.
        /// </summary>
        public static readonly GameVersionFlavor Classic = new GameVersionFlavor(GameVersionFlavorEnum.Classic, "wow_classic");

        static GameVersionFlavor()
        {
            var gameVersionFlavorMap = new Dictionary<GameVersionFlavorEnum, GameVersionFlavor>
            {
                { GameVersionFlavorEnum.Retail, Retail },
                { GameVersionFlavorEnum.Classic, Classic }
            };

            _gameVersionFlavorMap = new ReadOnlyDictionary<GameVersionFlavorEnum, GameVersionFlavor>(gameVersionFlavorMap);
        }

        private GameVersionFlavor(GameVersionFlavorEnum gameVersionFlavorEnum, String curseforgeCode)
        {
            this._gameVersionFlavorEnum = gameVersionFlavorEnum;
            this._name = gameVersionFlavorEnum.ToString();
            this._curseforgeCode = curseforgeCode;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override String ToString() => this._name;

        /// <summary>
        /// Returns the Cursforge code that represents the current object.
        /// </summary>
        /// <returns>The Cursforge code that represents the current object.</returns>
        public String ToCurseforgeCode() => this._curseforgeCode;

        /// <summary>
        /// Returns the Game Version Flavor associated to the specified enum.
        /// </summary>
        /// <param name="gameVersionFlavorEnum">The specified enum.</param>
        /// <returns></returns>
        public static GameVersionFlavor FromEnum(GameVersionFlavorEnum gameVersionFlavorEnum) => _gameVersionFlavorMap[gameVersionFlavorEnum];

        /// <summary>
        /// Returns an enum that represents the Game Version Flavor object.
        /// </summary>
        /// <param name="gameVersionFlavor">The Game Version Flavor object</param>
        public static implicit operator GameVersionFlavorEnum(GameVersionFlavor gameVersionFlavor) => gameVersionFlavor._gameVersionFlavorEnum;

        /// <summary>
        /// The enum from which the <see cref="GameVersionFlavor"/> constructs.
        /// </summary>
        public enum GameVersionFlavorEnum
        {
            Retail,
            Classic
        }
    }
}