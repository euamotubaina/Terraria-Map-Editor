using TEdit.Common.Reactive;
using TEdit.Terraria;
using TEdit.Geometry;
using TEdit.Configuration;

namespace TEdit.UI;

public class MouseTile : ObservableObject
{
    private TileMouseState _mouseState = new TileMouseState();
    private Tile _tile;
    private Vector2Short _uV;
    private string _tileExtras;
    private string _tileName;
    private string _wallName;
    private string _paint;

    public TileMouseState MouseState
    {
        get { return _mouseState; }
        set { Set(nameof(MouseState), ref _mouseState, value); }
    }

    public string WallName
    {
        get { return _wallName; }
        set { Set(nameof(WallName), ref _wallName, value); }
    }

    public string TileName
    {
        get { return _tileName; }
        set { Set(nameof(TileName), ref _tileName, value); }
    }

    public string Paint
    {
        get { return _paint; }
        set { Set(nameof(Paint), ref _paint, value); }
    }

    public string TileExtras
    {
        get { return _tileExtras; }
        set { Set(nameof(TileExtras), ref _tileExtras, value); }
    }

    public Vector2Short UV
    {
        get { return _uV; }
        set { Set(nameof(UV), ref _uV, value); }
    }

    public Tile Tile
    {
        get { return _tile; }
        set
        {
            Set(nameof(Tile), ref _tile, value);

            if (WorldConfiguration.TileProperties.Count > _tile.Type)
            {
                Terraria.Objects.TileProperty tileProperty = WorldConfiguration.TileProperties[_tile.Type];
                TileName = tileProperty.Name;

                // TODO: add sprite names here
                TileName = _tile.IsActive ? $"{TileName} ({_tile.Type})" : "[empty]";
            }
            else
                TileName = $"INVALID TILE ({_tile.Type})";

            if (WorldConfiguration.WallProperties.Count > _tile.Wall)
                WallName = $"{WorldConfiguration.WallProperties[_tile.Wall].Name} ({_tile.Wall})";
            else
                WallName = $"INVALID WALL ({_tile.Wall})";

            UV = new Vector2Short(_tile.U, _tile.V);
            if (_tile.LiquidAmount > 0)
            {
                TileExtras = $"{_tile.LiquidType}: {_tile.LiquidAmount}";
            }
            else
                TileExtras = string.Empty;

            if (_tile.TileColor > 0)
            {
                if (_tile.WallColor > 0)
                    Paint =
                        $"Tile: {WorldConfiguration.PaintProperties[_tile.TileColor].Name}, Wall: {WorldConfiguration.PaintProperties[_tile.WallColor].Name}";
                else
                    Paint = $"Tile: {WorldConfiguration.PaintProperties[_tile.TileColor].Name}";
            }
            else if (_tile.WallColor > 0)
            {
                Paint = $"Wall: {WorldConfiguration.PaintProperties[_tile.WallColor].Name}";
            }
            else
            {
                Paint = "None";
            }

            if (_tile.InActive)
            {
                TileExtras += " Inactive";
            }

            if (_tile.Actuator)
            {
                TileExtras += " Actuator";
            }

            if (_tile.WireRed || _tile.WireBlue || _tile.WireGreen || _tile.WireYellow)
            {
                if (!string.IsNullOrWhiteSpace(TileExtras))
                    TileExtras += ", Wire ";
                else
                    TileExtras += "Wire ";
                if (_tile.WireRed)
                    TileExtras += "R";
                if (_tile.WireGreen)
                    TileExtras += "G";
                if (_tile.WireBlue)
                    TileExtras += "B";
                if (_tile.WireYellow)
                    TileExtras += "Y";
            }
        }
    }
}
