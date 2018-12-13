using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Team : MonoBehaviour
{

    public Player Side;

    public List<GridRow> Rows;
    public GridRow RowPrefab;

    BattleGrid grid;

    public Player PlayerType;


    public delegate void AttackDelegate(Player side);
    public static event AttackDelegate OnAttackFinish;

    private void Start()
    {
        grid = FindObjectOfType<BattleGrid>();
    }

    [ExecuteInEditMode]
    public void Init(Player side)
    {

        Side = side;

        for (int i = 0; i < 4; i++)
        {
            GridRow row = Instantiate(RowPrefab, transform);
            row.InitializeSlots(Side, i);
            Rows.Add(row);
            row.name = "Row " + i;
        }
    }

    [ExecuteInEditMode]
    public void Unload()
    {

        List<Transform> toRemove = new List<Transform>();
        foreach (Transform t in GetComponentInChildren<Transform>(true))
        {
            toRemove.Add(t);
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            DestroyImmediate(toRemove[i].gameObject);
        }
        Rows.Clear();
    }

    public IEnumerator Attack()
    {
        for (int i = Rows.Count - 1; i >= 0; i--)
        {
            for (int s = Rows[i].Slots.Count - 1; s >= 0; s--)
            {
                GridSlot slot = Rows[i].Slots[s];
                if (!slot.IsEmpty)
                {
                    GridRow opponentRow = grid.GetOpponentRow(Side, i);
                    GridSlot target = opponentRow.GetFirstTarget(Side);
                    if (target != null)
                    {
                        Debug.Log("Target: " + target.Champion.Data.Name);
                        Vector3 pos = slot.Champion.transform.position;
                        slot.Champion.transform.DOMove(target.Champion.transform.position, GameManager.Instance.AttackDuration / 2).OnComplete(() => 
                        {
                            slot.Champion.transform.DOMove(pos, GameManager.Instance.AttackDuration / 2);
                            target.TakeDamage(slot.Champion.Data.Damage);
                        });
                        //animate
                    }
                    else
                    {
                        Debug.Log("Target was not found");
                        Castle c = grid.GetOpponentCastle(Side);
                        Vector3 pos = slot.Champion.transform.position;
                        slot.Champion.transform.DOMove(c.transform.position, GameManager.Instance.AttackDuration / 2).OnComplete(() =>
                        {
                            slot.Champion.transform.DOMove(pos, GameManager.Instance.AttackDuration/2);
                            c.TakeDamage(slot.Champion.Data.Damage);
                        });
                        

                        //animate
                    }
                    yield return new WaitForSeconds(GameManager.Instance.AttackDuration);
                }
            }
        }
        if (OnAttackFinish != null)
        {
            OnAttackFinish(Side);
        }
        yield return new WaitForSeconds(1f);
    }
}
