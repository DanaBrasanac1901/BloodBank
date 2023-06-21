import { Injectable } from "@angular/core";
import { Observable, Observer } from 'rxjs';
import { AnonymousSubject } from 'rxjs/internal/Subject';
import { Subject } from 'rxjs';
import { map } from 'rxjs/operators';

const CHAT_URL = "ws://localhost:16177/ws";

//dict that defines the behavior of an object, but does not specify its content
export interface Message {
    source: string;
    content: string;
}

@Injectable()
export class WebsocketService {
    //an instance of AnonymousSubject class with MessageEvent property
    //The AnonymousSubject class allows extending a Subject by defining the source and destination observables
    private subject: AnonymousSubject<MessageEvent>;
    //instance of Subject class. Every Subject is an Observable and an Observer
    //We'll subscribe to this Subject, and we'll be able to call next to feed values as well as error and complete.
    public messages: Subject<Message>;

    constructor() {
        this.messages = <Subject<Message>>this.connect(CHAT_URL).pipe(
            map(
                (response: MessageEvent): Message => {
                    console.log(response.data);
                    
                    let data = JSON.parse(response.data)
                    return data;
                }
            )
        );
    }

    //validates if subject property doesn’t exist and then calls create method for creating the subject
    public connect(url:any): AnonymousSubject<MessageEvent> {
        if (!this.subject) {
            console.log("establishing connection...");
            this.subject = this.create(url);
            console.log("Successfully connected: " + url);
        }
        console.log("has a connection");
        return this.subject;
    }

    //creates the AnonymousSubject that will be used for subscriptions
    private create(url:any): AnonymousSubject<MessageEvent> {
        let ws = new WebSocket(url);
        let observable = new Observable((obs: Observer<MessageEvent>) => {
            ws.onmessage = obs.next.bind(obs);
            ws.onerror = obs.error.bind(obs);
            ws.onclose = obs.complete.bind(obs);
            return ws.close.bind(ws);
        });
        let observer = {
            error: ()=>{},
            complete: ()=>{},
            next: (data: Object) => {
                console.log('Message sent to websocket: ', data);
                if (ws.readyState === WebSocket.OPEN) {
                    ws.send(JSON.stringify(data));
                }
            }
        };
        return new AnonymousSubject<MessageEvent>(observer, observable);
    }
}