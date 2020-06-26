using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumClass
{
    public enum eTypeIconSprite
    {
        Eggplant        = 0,
        Spider,
        Mirror,
        Pepper,
        TeddyBear,
        BeachBall,
        Giraffe,
        Crab,
        Leaf,
        ButterFly,
        Ribon,
        SnowMan,
        Carrot,
        Strawberry,
        Peanut,

        max
    }

    public enum eTypeScene
    {
        Start       = 0,
        Home,
        Ingame
    }

    public enum eStateLoadding
    {
        none        = 0,
        UnLoad,
        Load,
        Loading,
        LoadingDone,
        EndLoad
    }
}
