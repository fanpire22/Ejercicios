using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentJump : MonoBehaviour
{

    NavMeshAgent _agent;
    bool _jumping;

    // Use this for initialization
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.isOnOffMeshLink && !_jumping)
        {
            float time = Vector3.Distance(this.transform.position, _agent.currentOffMeshLinkData.endPos) * 0.2f;

            StartCoroutine(Parabola(_agent, _agent.currentOffMeshLinkData.endPos.y + 1,time));

        }
    }

    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        _jumping = true;

        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;

            yield return null;
        }
        _agent.CompleteOffMeshLink();

        _jumping = false;
    }
}
