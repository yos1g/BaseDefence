using UnityEngine;

[ExecuteInEditMode]
public class BaseGuiWindow : MonoBehaviour
{

	public void DoWindowTitle(Rect windowRect, string text)
	{
		Vector2 bgOffset = new Vector2(36.0f, 15.0f);
		// Determine the width and height of the title background
		float bgWidth = windowRect.width - (2 * bgOffset.x);
		float bgHeight = 91.0f;
		Rect windowTitleBGRect = new Rect(bgOffset.x, bgOffset.y, bgWidth, bgHeight);
		
		// Lay the title background
		GUI.Label(windowTitleBGRect, "", "windowTitleBackground");
		
		Vector2 titleOffset0 = new Vector2(0.0f, 56.0f);
		Vector2 titleOffset1 = new Vector2(2.0f, 53.0f);
		Vector2 titleOffset2 = new Vector2(-3.0f, 57.0f);
		Vector2 titleOffset3 = new Vector2(1.0f, 55.0f);
		Vector2 titleOffset4 = new Vector2(4.0f, 53.0f);
		Vector2 titleSize = new Vector2(windowRect.width, 29.0f);
		
		// Draw the title
		GUI.Label(new Rect(titleOffset0.x, titleOffset0.y, titleSize.x, titleSize.y), text, "windowTitle");
		GUI.Label(new Rect(titleOffset1.x, titleOffset1.y, titleSize.x, titleSize.y), text, "windowTitleShadow");
		GUI.Label(new Rect(titleOffset2.x, titleOffset2.y, titleSize.x, titleSize.y), text, "windowTitleShadow");
		GUI.Label(new Rect(titleOffset3.x, titleOffset3.y, titleSize.x, titleSize.y), text, "windowTitleShadow");
		GUI.Label(new Rect(titleOffset4.x, titleOffset4.y, titleSize.x, titleSize.y), text, "windowTitleShadow");
		
		Vector2 olOffset  = new Vector2(37.0f, 41.0f);
		// Determine the width and height of the title overlay
		float olWidth = windowRect.width - (2 * olOffset.x);
		float olHeight = 55;
		Rect windowTitleOLRect = new Rect(olOffset.x, olOffset.y, olWidth, olHeight);
		
		// Lay the title overlay
		GUI.Label(windowTitleOLRect, "", "windowTitleOverlay");
	}

	public void DoLabel(Rect r, string text)
	{
		GUIStyle LabelStyle = GUI.skin.GetStyle("label");
		GUIStyle LabelShadowStyle = GUI.skin.GetStyle("labelTextShadow");
		
		this.DoTextWithShadow(r, new GUIContent(text), LabelStyle, LabelStyle.normal.textColor, LabelShadowStyle.normal.textColor, new Vector2(1.0f, 1.0f));
	}

	public void DoTextWithShadow(Rect rect, GUIContent content, GUIStyle style, Color txtColor, Color shadowColor, Vector2 direction)
	{
		GUIStyle backupStyle = new GUIStyle(style);
		
		style.normal.textColor = shadowColor;
		rect.x += direction.x;
		rect.y += direction.y;
		GUI.Label(rect, content, style);
		
		style.normal.textColor = txtColor;
		rect.x -= direction.x;
		rect.y -= direction.y;
		GUI.Label(rect, content, style);
		
		style = backupStyle;
	}

	public void DoImage(Vector2 offset, Texture2D imageTexture)
	{
		Vector2 frameSize = new Vector2(imageTexture.width + 8.0f, imageTexture.height + 8.0f);

		GUI.BeginGroup(new Rect(offset.x, offset.y, frameSize.x, frameSize.y));
		GUI.Label(new Rect(0.0f, 0.0f, frameSize.x, frameSize.y), "", "imageFrame");
		GUI.DrawTexture(new Rect(4.0f, 4.0f, imageTexture.width, imageTexture.height), imageTexture);
		GUI.EndGroup();
	}

	public Rect ScaleRect(Rect rect, float scale)
	{
		scale = scale * 100;
		
		Rect newRect = new Rect(0, 0, 0, 0);
		newRect.x = Mathf.CeilToInt((rect.x / 100) * scale);
		newRect.y = Mathf.CeilToInt((rect.y / 100) * scale);
		newRect.width = Mathf.CeilToInt((rect.width / 100) * scale);
		newRect.height = Mathf.CeilToInt((rect.height / 100) * scale);

		return newRect;
	}

	public void DoSeparator(Vector2 offset)
	{
		GUI.Label(new Rect(offset.x, offset.y, 340.0f, 16.0f), "", "separator");
	}

	public void DoTextContainerTitle(Rect r, string text)
	{
		GUIStyle TextStyle = GUI.skin.GetStyle("textContainerTitle");
		
		this.DoTextWithShadow(r, new GUIContent(text), TextStyle, TextStyle.normal.textColor, TextStyle.hover.textColor, new Vector2(2.0f, 1.0f));
	}

	public void DoTextContainerText(Rect r, string text)
	{
		GUIStyle TextStyle = GUI.skin.GetStyle("textContainerText");
		
		this.DoTextWithShadow(r, new GUIContent(text), TextStyle, TextStyle.normal.textColor, TextStyle.hover.textColor, new Vector2(2.0f, 1.0f));
	}

	public bool DoButton(Rect r, string content)
	{
		GUIStyle ButtonTextStyle  = GUI.skin.GetStyle("buttonText");
		GUIStyle backupStyle  = new GUIStyle(ButtonTextStyle);
		GUIStyle ShadowStyle  = GUI.skin.GetStyle("buttonTextShadow");
		
		Rect size = new Rect(0, 0, r.width, r.height);
		Rect buttonRect = new Rect(13, 13, (r.width - (13 * 2)), (r.height - (13 * 2)));
		
		GUI.BeginGroup(r);
		
			bool result = GUI.Button(buttonRect, "");
			Color color = (buttonRect.Contains(Event.current.mousePosition) && Input.GetMouseButton(0)) ? ButtonTextStyle.active.textColor : (buttonRect.Contains(Event.current.mousePosition) ? ButtonTextStyle.hover.textColor : ButtonTextStyle.normal.textColor);
			Color colorShadow = (buttonRect.Contains(Event.current.mousePosition) && Input.GetMouseButton(0)) ? ShadowStyle.active.textColor : (buttonRect.Contains(Event.current.mousePosition) ? ShadowStyle.hover.textColor : ShadowStyle.normal.textColor);
			Vector2 direction = new Vector2(0.0f, 1.0f);
			DoTextWithShadow(size, new GUIContent(content), ButtonTextStyle, color, colorShadow, direction);
			
		GUI.EndGroup();
		
		ButtonTextStyle.normal.textColor = backupStyle.normal.textColor;
		return result;
	}

}
