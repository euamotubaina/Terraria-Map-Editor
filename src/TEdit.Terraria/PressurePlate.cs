using System;
using TEdit.Common.Reactive;

namespace TEdit.Terraria;

public class PressurePlate : ObservableObject
{
    private int _x;
    private int _y;

    public int PosX
    {
        get { return _x; }
        set { Set(nameof(PosX), ref _x, value); }
    }
    
    public int PosY
    {
        get { return _y; }
        set { Set(nameof(PosY), ref _y, value); }
    }        
    
}
