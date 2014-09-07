using UnityEngine;
using System;
using System.Collections.Generic;
using WebSocketSharp;
using SimpleJSON;

public class WebSocketClient : MonoBehaviour {

    public string ws_url = "ws://192.168.1.221:8000/";

    public float force_consume_delay = 50.0f / 1000.0f;
    public float force_scale = 0.005f;
    public float drag = 0.975f; // simple multiplier...

    Queue<int> force_queue = null;
    string message_json = "";
    WebSocket ws = null;

    float next_force_consume = 0.0f;
    Vector3 velocity = Vector3.zero;
    Vector3 direction = Vector3.forward;

	void Start() {

        force_queue = new Queue<int>();

        using (ws = new WebSocket(ws_url)) {
            ws.OnOpen += (sender, e) => {
                Debug.Log("[CONNECTED] " + ws_url);
            };

            ws.OnClose += (sender, e) => {
                Debug.Log("[DISCONNECTED]");
            };

            ws.OnMessage += (sender, e) => {
                Debug.Log("[RECEIVED] " + e.Data);
                message_json = e.Data;
            };

            ws.OnError += (Sender, e) => {
                Debug.Log("[ERROR] Websocket error");
            };
        }

        try {
            ws.Connect();
        } catch(Exception error) {
            Debug.Log("[ERROR] " + error.Message);
        }
	}

    void HandleMessage() {
        if(message_json != "") {
            var message = JSON.Parse(message_json);

            switch(message["type"]) {
                case "TXT":
                    break;
                case "STROKE_FORCE":
                    QueueForce(message["content"]["forceplot"].AsArray);
                    break;
                case "STROKE_START":
                    break;
                case "STROKE_END":
                    break;
                case "WORKOUT_START":
                    break;
                case "WORKOUT_END":
                    break;
                default:
                    break;
            }

            message_json = "";
        }
    }
	
	void Update() {
        HandleMessage();

        if (force_queue.Count > 0 && Time.time >= next_force_consume) {
            int force = force_queue.Dequeue();
            velocity += direction * force * force_scale;
            next_force_consume = Time.time + force_consume_delay; 
        }

        transform.position += velocity * Time.deltaTime;
        velocity *= drag;
	}

    void QueueForce(JSONArray forceplot) {
        for(int f = 0; f < forceplot.Count; ++f) {
            force_queue.Enqueue(forceplot[f].AsInt);
        }
    }
}
