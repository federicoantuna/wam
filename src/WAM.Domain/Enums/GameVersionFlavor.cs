using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WAM.Domain.Enums
{
    /// <summary>
    /// Game version flavor options.
    /// </summary>
    public class GameVersionFlavor
    {
        private const String RetailCode = "WOW_RETAIL";
        private const String ClassicCode = "WOW_CLASSIC";

        private static readonly IReadOnlyDictionary<GameVersionFlavorEnum, GameVersionFlavor> _gameVersionFlavorEnumMap;
        private static readonly IReadOnlyDictionary<String, GameVersionFlavor> _gameVersionFlavorCodeMap;

        private readonly GameVersionFlavorEnum _gameVersionFlavorEnum;
        private readonly String _name;
        private readonly String _curseforgeCode;

        /// <summary>
        /// Option for the Retail flavor of the game.
        /// </summary>
        public static readonly GameVersionFlavor Retail = new GameVersionFlavor(GameVersionFlavorEnum.Retail, RetailCode);

        /// <summary>
        /// Option for the Classic flavor of the game.
        /// </summary>
        public static readonly GameVersionFlavor Classic = new GameVersionFlavor(GameVersionFlavorEnum.Classic, ClassicCode);

        static GameVersionFlavor()
        {
            var gameVersionFlavorEnumMap = new Dictionary<GameVersionFlavorEnum, GameVersionFlavor>
            {
                { GameVersionFlavorEnum.Retail, Retail },
                { GameVersionFlavorEnum.Classic, Classic }
            };

            var gameVersionFlavorCodeMap = new Dictionary<String, GameVersionFlavor>
            {
                { RetailCode, Retail },
                { ClassicCode, Classic }
            };

            _gameVersionFlavorEnumMap = new ReadOnlyDictionary<GameVersionFlavorEnum, GameVersionFlavor>(gameVersionFlavorEnumMap);
            _gameVersionFlavorCodeMap = new ReadOnlyDictionary<String, GameVersionFlavor>(gameVersionFlavorCodeMap);
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
        public String ToCurseforgeCode() => this._curseforgeCode.ToLowerInvariant();

        /// <summary>
        /// Returns the Game Version Flavor associated to the specified enum.
        /// </summary>
        /// <param name="gameVersionFlavorEnum">The specified enum.</param>
        /// <returns></returns>
        public static GameVersionFlavor FromEnum(GameVersionFlavorEnum gameVersionFlavorEnum) => _gameVersionFlavorEnumMap[gameVersionFlavorEnum];

        /// <summary>
        /// Returns the Game Version Flavor associated to the specified code.
        /// </summary>
        /// <param name="gameVersionFlavorCode">The specified code.</param>
        /// <returns></returns>
        public static GameVersionFlavor FromCode(String gameVersionFlavorCode) => _gameVersionFlavorCodeMap[gameVersionFlavorCode.ToUpperInvariant()];

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