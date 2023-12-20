using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInCloset : MonoBehaviour
{
    public GameObject player; // Pemain yang akan bersembunyi
    private Vector2 originalPlayerPosition; // Posisi pemain sebelum bersembunyi
    private Vector2 originalPlayerScale; // Skala pemain sebelum bersembunyi
    public KeyCode hideKey = KeyCode.E; // Tombol untuk bersembunyi
    public float interactionRange = 2f; // Jarak pemain untuk berinteraksi dengan lemari
    private bool isHiding = false;

    // Tambahkan referensi ke komponen Animator
    private Animator closetAnimator;

    // Tambahkan referensi ke komponen Cinemachine
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        // Simpan posisi awal pemain
        originalPlayerPosition = player.transform.position;
        originalPlayerScale = player.transform.localScale;

        // Dapatkan komponen Animator pada GameObject lemari
        closetAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Jika tombol hideKey ditekan dan pemain tidak sedang bersembunyi
        if (Input.GetKeyDown(hideKey) && !isHiding && IsPlayerNearby())
        {
            StartCoroutine(HidePlayerCoroutine());
        }
        // Jika tombol hideKey ditekan dan pemain sedang bersembunyi
        else if (Input.GetKeyDown(hideKey) && isHiding)
        {
            StartCoroutine(UnhidePlayerCoroutine());
        }
    }

    bool IsPlayerNearby()
    {
        // Periksa apakah pemain berada dalam jarak interaksi
        float distance = Vector2.Distance(player.transform.position, transform.position);
        return distance <= interactionRange;
    }

    IEnumerator HidePlayerCoroutine()
    {
        // Simpan posisi awal pemain sebelum bersembunyi
        originalPlayerPosition = player.transform.position;

        // Aktifkan animasi lemari terbuka
        closetAnimator.SetBool("IsOpen", true);

        // Tunggu beberapa detik agar animasi terbuka selesai
        yield return new WaitForSeconds(closetAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Sembunyikan pemain di posisi lemari
        player.transform.position = transform.position;
        isHiding = true;

        // Skala pemain diubah untuk memberikan efek "masuk ke dalam"
        player.transform.localScale = new Vector2(0.5f, 0f);

        // Matikan virtual camera Cinemachine
        virtualCamera.Priority = 0;
    }

    IEnumerator UnhidePlayerCoroutine()
    {
        // Kembalikan pemain ke posisi sebelum bersembunyi
        player.transform.position = originalPlayerPosition;
        isHiding = false;

        // Skala pemain dikembalikan ke skala semula
        player.transform.localScale = originalPlayerScale;

        // Aktifkan animasi lemari tertutup
        closetAnimator.SetBool("IsOpen", false);

        // Tunggu beberapa detik agar animasi tertutup selesai
        yield return new WaitForSeconds(closetAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Aktifkan kembali virtual camera Cinemachine
        virtualCamera.Priority = 10;
    }
}
