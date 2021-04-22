using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalRoomOption : MonoBehaviour
{
    [Header("Dependences")]
    public PlaftormController trap;
    [Header("Information")]
    public int opcion;
    public bool choosed;

    public void activarTrampa() 
    {
        trap.active = true;
    }

    public void desactivarTrampa() 
    {
        trap.active = false;
    }
}
