using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniStatusWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _textName;
    [SerializeField]
    Slider _gaugeHP;
    [SerializeField]
    Slider _gaugeShield;
    [SerializeField]
    Slider _gaugeBullet;
#pragma warning restore

    public void InitSetting(string name, float hprate, float shieldrate, float bulletrate)
    {
        _textName.text = name;
        SettingHPSlider(hprate);
        SettingShieldSlider(shieldrate);
        SettingBulletSlider(bulletrate);
    }

    public void SettingHPSlider(float hprate)
    {
        _gaugeHP.value = hprate;
    }

    public void SettingShieldSlider(float shieldrate)
    {
        _gaugeShield.value = shieldrate;
    }

    public void SettingBulletSlider(float bulletrate)
    {
        _gaugeBullet.value = bulletrate;
    }
}
