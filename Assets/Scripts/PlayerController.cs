using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeSolvingAlgorithmTremaux
{
    public class PlayerController : MonoBehaviour
    {
        private Animator animator;

        public float speed = 20f;

        // Trémaux Alghoritm
        [Tooltip("Position where player start.")]
        [SerializeField] Waypoint actualWaypoint;
        int layerMask;
        Waypoint destiny;


        void Start()
        {
            if (!actualWaypoint) { return; }
            actualWaypoint.MarkWaypoint();
            destiny = ChooseDestiny();
            layerMask = ~(LayerMask.GetMask("Player"));
            animator = GetComponent<Animator>();
            transform.position = actualWaypoint.transform.position;
        }

        void Update()
        {
            if (destiny)
            {
                StartCoroutine(Move());   
            } else
            {
                StopCoroutine(Move());
                if (!actualWaypoint.IsGoal())
                {
                    destiny = ChooseDestiny();
                } else
                {
                    SceneController sceneController = FindObjectOfType<SceneController>();
                    if(sceneController)
                    {
                        sceneController.EndGame();
                    }
                }
            }
                
           
        }

        IEnumerator Move()
        {
            while (true)
            {
                
                Debug.Log(actualWaypoint.name + " > " + destiny.name);
                UpdateAnimation(destiny.transform);
                transform.position = Vector3.Lerp(transform.position, destiny.transform.position, 1 / (speed * (Vector3.Distance(transform.position, destiny.transform.position))));
                float timeDistance = Vector2.Distance(transform.position, destiny.transform.position) / speed;
                yield return new WaitForSeconds(timeDistance);
                if (Vector2.Distance(destiny.gameObject.transform.position, transform.position) < Vector2.kEpsilon)
                {
                    actualWaypoint.MarkWaypoint();
                    actualWaypoint = destiny;
                    actualWaypoint.MarkWaypoint();
                    destiny = null;
                }
            }

        }

        private Waypoint ChooseDestiny()
        {
            Waypoint[] waypointsList = actualWaypoint.GetWaypointsConnected();
            if (waypointsList.Length == 0) { return null; }
            if (waypointsList.Length == 1) //Beco
            {
                actualWaypoint.NoWayOut();
                waypointsList[0].DesmarkWaypoint();
                return waypointsList[0];
            }
            Waypoint destiny;
            byte waysCheckedState2 = 0; //Para vêr se todos os caminhos disponiveis ja foram visitados duas vezes
            byte waysCheckedState1 = 0; //Para vêr se todos os caminhos disponiveis ja foram visitados uma vez
            byte waysWithNoExit = 0; //Para corredores que levam a lado nenhum
            byte preference = 0; //Ele começa preferindo caminho State 0 (Não visitados), no entanto se todos ja tiverem sido visitido uma vez, ele mudara para 1 (Visitado uma vez)
            do
            {
                if (waysCheckedState2 > waypointsList.Length)
                {
                    return null;
                } else if (waysCheckedState1 >= waypointsList.Length)
                {
                    preference = 1;
                }
                
                if(waysWithNoExit == waypointsList.Length || actualWaypoint.hasNoWayOut)
                {
                    actualWaypoint.NoWayOut();
                    return FindWayWithExit(waypointsList);
                }

                destiny = waypointsList[Random.Range(0, waypointsList.Length)];
                
                if(destiny.HasNoWayOut())
                {
                    waysWithNoExit++;
                }

                if (destiny.GetState() >= 2)
                {
                    waysCheckedState2++;
                } else if (destiny.GetState() == 1)
                {
                    waysCheckedState1++;
                }

            } while (destiny.GetState() != preference);
            return destiny;
        }

        private Waypoint FindWayWithExit(Waypoint[] ways)
        {
            //Debug.Log(actualWaypoint.name + "AQUI");
            foreach (Waypoint w in ways)
            {
                //Debug.Log(actualWaypoint.name + " VENDO " + w.name);
                if (!w.HasNoWayOut())
                {
                    //Debug.Log(actualWaypoint.name + " VENDO FUNDO " + w.name);
                    return w;
                } 
            }
            return null;
        }
        
        void UpdateAnimation(Transform destiny)
        {
            if (destiny.position.y > transform.position.y)
            {
                animator.SetInteger("direction", 0);
                return;
            }
            else if (destiny.position.y < transform.position.y)
            {
                animator.SetInteger("direction", 1);
                return;
            }
            else if (destiny.position.x > transform.position.x)
            {
                animator.GetComponent<SpriteRenderer>().flipX = false;

                animator.SetInteger("direction", 2);
                return;
            }
            else if (destiny.position.x < transform.position.x)
            {
                animator.GetComponent<SpriteRenderer>().flipX = true;
                animator.SetInteger("direction", 2);
                return;
            }

        }
    }
}