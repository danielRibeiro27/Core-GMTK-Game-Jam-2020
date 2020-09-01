using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCREDITOSManager : MonoBehaviour
{
    [SerializeField] private LevelManager lvManager;
    [SerializeField] private Image blackPanel;
    [SerializeField] private Image mascara;
    [SerializeField] private Sprite mascara_olho_aberto;
    [SerializeField] private Animator creditosAnim;
    [SerializeField] private GameObject numero_tres;
    private DialogueTrigger currentTrigger = null;
    private bool control = false;
    private bool esta_nos_creditos = false;

    [SerializeField] private GameObject creditos;
    [SerializeField] private GameObject mascara_final;
    [SerializeField] private GameObject numero3;


    void Start()
    {
        control = true;
        StartCoroutine(SpriteFade(0f, 3f));

        AudioManager.instance.StopByName("BossMusic");
        AudioManager.instance.PlayByName("CreditosMusic");
    }

    private void Update()
    {
        //Verifica se a animação de créditos acabou
        if (creditosAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !creditosAnim.IsInTransition(0) && !control && esta_nos_creditos)
        {
            lvManager.GoToLevel(0);
        }
    }


    private IEnumerator SpriteFade(float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = blackPanel.color.a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            blackPanel.color = new Color(blackPanel.color.r, blackPanel.color.g, blackPanel.color.b, newAlpha);
            yield return null;
        }

        StartConversation("Primeira conversa LvCREDITOS");
    }

    private void StartConversation(string triggerName, bool? canMove = null, bool? canInput = null)
    {
        //busca o trigger corespondente
        DialogueTrigger[] triggers = FindObjectsOfType<DialogueTrigger>();
        DialogueTrigger targetTrigger = null;
        foreach (DialogueTrigger tri in triggers)
        {
            if (tri.dialogueName == triggerName)
            {
                targetTrigger = tri;
                break;
            }
        }

        //ativa os dialogos um após o outro
        if (targetTrigger != null)
        {
            targetTrigger.TriggerDialogue(canMove, canInput);
            currentTrigger = targetTrigger;
        }

    }

    public IEnumerator TrocarMascara()
    {
        mascara.sprite = mascara_olho_aberto;
        numero_tres.SetActive(true);
        AudioManager.instance.PlayByName("Mascara acendendo");

        yield return new WaitForSeconds(3f);

        esta_nos_creditos = true;
        creditosAnim.SetTrigger("SubirCreditos");
        creditos.SetActive(true);
        mascara_final.SetActive(false);
        numero3.SetActive(false);
        control = false;

    }
}
