using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum DamageType { Normal, Fire, Earth, Thunrder, Ice }

public interface IDamagable
{
    float Health { get; set; }
    float MaxHealth { get; set; }
    float Armor { get; set; }
    void GetDamage(DamageInfo damageInfo);
}

public struct DamageInfo
{
    public DamageType DamageType { get; set; } // 데미지 타입
    public float Amount { get; set; } // 데미지
    public bool IsAbsolute { get; set; } // 방어 무시
}