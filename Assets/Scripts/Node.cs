﻿using System;
using UnityEngine;

public class Node : MonoBehaviour {
    [SerializeField] private Transform gunPrefab;
    public CashManager CashManager { private get; set ; }
    private Transform gun;
    private Transform guns;
    public Func<bool> IsMouseClickAllowed { get; set; }
    public Action<NodeChangeEvent> NodeChangeListener { get ; set ; }

    private const int GunCostInDollars = 100;

    private void Start() {
        guns = transform.Find("/Instance Containers/Guns");
    }

    private void OnMouseDown() {
        if (! IsMouseClickAllowed()) return;
        if (gun == null) {
            if (CashManager.Buy(GunCostInDollars)) {
                NodeChangeListener(new GunAddedToNode());
                AddGun();
            }
        } else {
            CashManager.Receive(GunCostInDollars / 2);
            Destroy(gun.gameObject);
            gun = null;
        }
    }

    private void AddGun() {
        gun = Instantiate(gunPrefab, transform.position, Quaternion.identity);
        gun.SetParent(guns);
    }
}