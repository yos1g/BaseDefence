using UnityEngine;

class ColorHelper
{
    public static string ToHex(Color color)
    {
        var red = color.r * 255;
        var green = color.g * 255;
        var blue = color.b * 255;
        var a = ColorHelper.getHex((int)Mathf.Floor(red / 16));
        var b = ColorHelper.getHex((int)Mathf.Round(red % 16));
        var c = ColorHelper.getHex((int)Mathf.Floor(green / 16));
        var d = ColorHelper.getHex((int)Mathf.Round(green % 16));
        var e = ColorHelper.getHex((int)Mathf.Floor(blue / 16));
        var f = ColorHelper.getHex((int)Mathf.Round(blue % 16));

        string z = a + b + c + d + e + f;
        return z;
    }

    public static Color ToRGB (string color) {
        float red = (float)(ColorHelper.hexToInt(color[1]) + ColorHelper.hexToInt(color[0]) * 16.000) / 255;
        float green = (float)(ColorHelper.hexToInt(color[3]) + ColorHelper.hexToInt(color[2]) * 16.000) / 255;
        float blue = (float)(ColorHelper.hexToInt(color[5]) + ColorHelper.hexToInt(color[4]) * 16.000) / 255;
	    Color finalColor = new Color();
	    finalColor.r = red;
	    finalColor.g = green;
	    finalColor.b = blue;
	    finalColor.a = 1;
	    return finalColor;
    }

    public static string getHex(int dec)
    {
        char[] alpha = "0123456789ABCDEF".ToCharArray();
	    return alpha[dec].ToString();
    }

    public static int hexToInt (char hexChar) {
        switch (hexChar.ToString())
        {
		    case "0": return 0;
		    case "1": return 1;
		    case "2": return 2;
		    case "3": return 3;
		    case "4": return 4;
		    case "5": return 5;
		    case "6": return 6;
		    case "7": return 7;
		    case "8": return 8;
		    case "9": return 9;
		    case "A": return 10;
		    case "B": return 11;
		    case "C": return 12;
		    case "D": return 13;
		    case "E": return 14;
		    case "F": return 15;
	    }

        return 0;
    }
}

