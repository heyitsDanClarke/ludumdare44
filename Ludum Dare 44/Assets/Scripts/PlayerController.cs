﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 7;
    public float maxFallSpeed = 7;
    public float acceleration = 1;

    Rigidbody2D _rb;
    SpriteRenderer _sr;
    Animator _anim;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.55f), Vector2.down, 0.1f);
        Debug.DrawRay(new Vector2(transform.position.x,transform.position.y-0.5f), Vector2.down, Color.red);
        if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
            _anim.SetBool("Falling", false);
        else
            _anim.SetBool("Falling", true);

        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, -Mathf.Max(_rb.velocity.y,maxFallSpeed));
        if (move.x < 0)
            _sr.flipX = true;
        else if(move.x > 0)
            _sr.flipX = false;
        _anim.SetFloat("WalkSpeed", Mathf.Abs(move.x));
        _rb.velocity = move;

        if (Input.GetKeyDown(KeyCode.B))
            Blink();
        if (Input.GetKeyDown(KeyCode.V))
            Shift();
    }

    void Blink()
    {
        Vector3 blinkDir = Vector2.down;

        transform.position += blinkDir;
    }

    void Shift()
    {
        Vector3 shiftDir = Vector3.zero;

        if (!_sr.flipX)
            shiftDir = Vector3.right * 3;
        else
            shiftDir = Vector3.left * 3;
        transform.position += shiftDir;
    }
}