
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    PlayerControl playerControl;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
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
        foreach(Image heart in hearts){
            heart.sprite = emptyHeart;
        }
        float ii = 0;
        for(float i = 0; i < playerControl.health; i+=1f){
            if(hearts[Mathf.FloorToInt(ii)].sprite == halfHeart){
                hearts[Mathf.FloorToInt(ii)].sprite = fullHeart;
            } else {
                hearts[Mathf.FloorToInt(ii)].sprite = halfHeart;
            }
            ii += 0.5f;
        }
        float size = map(playerControl.stamina,0,playerControl.maxStamina,0,staminaBackground.rectTransform.sizeDelta.x);
        staminaBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        staminaBar.rectTransform.ForceUpdateRectTransforms();
        //for(float i = playerControl.health; i < playerControl.maxHealth; i += 1){
        //    hearts[Mathf.RoundToInt(i)].sprite = emptyHeart;
        //}
    }
}
