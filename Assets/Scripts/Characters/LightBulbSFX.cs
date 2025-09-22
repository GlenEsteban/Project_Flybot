using UnityEngine;

public class LightBulbSFX : MonoBehaviour {
    // References    
    [SerializeField] AudioClip _sfxClip;
    private GrabbableItem _item;
    private AudioSource _audioSource;

    public void Awake() {
        _audioSource = GetComponent<AudioSource>();          
        _item = GetComponent<GrabbableItem>();

        _item.ItemBroke += PlayOneShotSFX;
    }

    public void PlayOneShotSFX() {
        _audioSource.PlayOneShot(_sfxClip);
    }
}
