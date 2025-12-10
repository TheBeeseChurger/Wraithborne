using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class WorldCardRecall : MonoBehaviour
{
    private readonly float _riseHeight = 0.8f;
    private readonly float _duration = 1.6f;

    private readonly List<Material> _materials = new();
    private readonly List<Color> _matOriCols = new();

    private readonly List<TextMeshProUGUI> _tmps = new();
    private readonly List<Color> _tmpOriCols = new();

    private void Awake()
    {
        CacheFadables();
    }

    private void CacheFadables()
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            var mat = r.material;
            _materials.Add(mat);
            _matOriCols.Add(mat.color);
        }

        GameObject canvas = GetComponentInChildren<Canvas>().gameObject;
        foreach (var tmp in canvas.GetComponentsInChildren<TextMeshProUGUI>())
        {
            _tmps.Add(tmp);
            _tmpOriCols.Add(tmp.color);
        }
    }

    public void Play(System.Action onRecallFin = null)
    {
        StartCoroutine(RecallAnimation(onRecallFin));
    }

    private IEnumerator RecallAnimation(System.Action onRecallFin)
    {
        float t = 0f;
        Vector3 startPos = transform.parent.position;
        Vector3 endPos = startPos + Vector3.up * _riseHeight;

        while (t < _duration)
        {
            t += Time.deltaTime;

            float p = Mathf.Clamp01(t / _duration);
            float eased = Mathf.SmoothStep(0f, 1f, p);

            transform.parent.position = Vector3.Lerp(startPos, endPos, eased);

            for (int i = 0; i < _materials.Count; i++)
            {
                Color c = _matOriCols[i];
                c.a = Mathf.Lerp(1f, 0f, eased);
                _materials[i].color = c;
            }

            for (int i = 0; i < _tmps.Count; i++)
            {
                Color c = _tmpOriCols[i];
                c.a = Mathf.Lerp(1f, 0f, eased);
                _tmps[i].color = c;
            }

            yield return null;
        }

        onRecallFin?.Invoke();
    }
}
