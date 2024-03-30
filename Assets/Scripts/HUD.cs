
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    PlayerControl playerControl;
    public Image healthBar;
    public RectTransform healthbarBackground;
    public Image staminaBar;
    public Image staminaBackground;
    public Image[] hearts;
    void Start(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        
    }
    float map(float val, float oldmin, float oldmax, float newmin, float newmax){
        return (val - oldmin) * (newmax - newmin) / (oldmax - oldmin) + newmin;
    }
    void Update()
    {
        
        float size = map(playerControl.health,0,playerControl.maxHealth,0,healthbarBackground.sizeDelta.x);
        healthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        healthBar.rectTransform.ForceUpdateRectTransforms();


        size = map(playerControl.stamina,0,playerControl.maxStamina,0,staminaBackground.rectTransform.sizeDelta.x);
        staminaBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        staminaBar.rectTransform.ForceUpdateRectTransforms();
        //for(float i = playerControl.health; i < playerControl.maxHealth; i += 1){
        //    hearts[Mathf.RoundToInt(i)].sprite = emptyHeart;
        //}
    }
}
