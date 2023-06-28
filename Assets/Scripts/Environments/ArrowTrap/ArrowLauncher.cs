using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour, ITriggerable
{
    [SerializeField] private int _arrowCount;
    [SerializeField] private bool _hasInfiniteArrow;

    //Use it if arrow will be launched after a few trigger.
    [SerializeField] private int _triggerCount;
    private int _remainingTriggerCount;

    [Space(10)]
    [SerializeField] private GameObject _arrow;

    //TODO: Oklari object pooling mantigina uygun sekilde olustur. Eger sinirsiz ok kullanilacaksa da bir ust sinir belirle.
    //Belki performans acisindan oklar ayri bir sinifin altinda toplanabilir ve arrowLauncher nesneleri oklari oradan ceker.

    //TODO: Eger belli bir tetiklenmeden sonra calisacaksa her tetiklenmede kalan tetiklenme sayisi azaltilmali ve 0'a ulastiginda ok firlatilmali.
    //Ok firlatildiktan sonra kalan tetiklenme sayisi resetlenmeli.
    public void HandleTrigger(Collider collider)
    {
        
    }
}
