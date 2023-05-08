using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private bool _isOpened;
    [SerializeField] private Animator _amimator;

   
    // Start is called before the first frame update


    public void Open()
    {
        _amimator.SetBool("IsOpened", _isOpened);
        _isOpened = !_isOpened;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
