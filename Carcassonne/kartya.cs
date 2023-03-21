using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    class kartya
    {
        string name = "";

        bool hasRoute = false;

        bool hasRouteUp = false;
        bool hasRouteDown = false;
        bool hasRouteLeft = false;
        bool hasRouteRight = false;
        

        bool hasCastle = false;

        bool hasCastleUp = false;
        bool hasCastleDown = false;
        bool hasCastleLeft = false;
        bool hasCastleRight = false;

        bool isSelected = false;


        public string Name { get => name; set => name = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public bool HasRoute { get => hasRoute; set => hasRoute = value; }
        public bool HasRouteUp { get => hasRouteUp; set => hasRouteUp = value; }
        public bool HasRouteDown { get => hasRouteDown; set => hasRouteDown = value; }
        public bool HasRouteLeft { get => hasRouteLeft; set => hasRouteLeft = value; }
        public bool HasRouteRight { get => hasRouteRight; set => hasRouteRight = value; }
        public bool HasCastle { get => hasCastle; set => hasCastle = value; }
        public bool HasCastleUp { get => hasCastleUp; set => hasCastleUp = value; }
        public bool HasCastleDown { get => hasCastleDown; set => hasCastleDown = value; }
        public bool HasCastleLeft { get => hasCastleLeft; set => hasCastleLeft = value; }
        public bool HasCastleRight { get => hasCastleRight; set => hasCastleRight = value; }
    }
}
