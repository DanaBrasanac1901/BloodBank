//src\app\app.component.ts
import { Component } from '@angular/core';
import { WebsocketService } from 'app/services/websocket.service';
import { Message } from 'app/services/websocket.service';
import { Location } from 'app/model/location.model';
import { DeliveryService } from 'app/services/delivery.service';
import Map from 'ol/Map';
import View from 'ol/View';
import OSM from 'ol/source/OSM';
import * as olProj from 'ol/proj';
import TileLayer from 'ol/layer/Tile';
import Overlay from 'ol/Overlay.js';

@Component({
  selector: "delivery-map-root",
  templateUrl: "./delivery-map.component.html",
  styleUrls: ["./delivery-map.component.css","ol.css"],
  providers: [WebsocketService]
})

export class DeliveryMapComponent {
  //socket config
  title = 'socketrv';
  content = '';
  received:string[]=[];
  sent:Message[] = [];


  public location:Location;

  private stop:boolean=false;

  public map:Map=new Map();
  
  

  constructor(private webSocket:  WebsocketService, private deliveryService:DeliveryService) {
    
    webSocket.messages.subscribe(msg => {
      this.received.push(JSON.stringify(msg));
      console.log(JSON.stringify(msg));
       let intermediary=msg as unknown;
       this.location=intermediary as Location;

        //console.log(this.markerdataSource);
      //console.log("Response from websocket: " + msg);      
    }); 
  }

  ngOnInit(){

    this.map = new Map({
      target: 'hotel_map',
      layers: [
        new TileLayer({
          source: new OSM(),
        }),
        
      ],
      view: new View({
        center: olProj.fromLonLat([19.835684294227466,45.25230882879536]),
        zoom: 13
      }),

      
    });
    
    


    
   }

  sendMsg() {
    

    let message = {
      source: '',
      content: ''
    };
    message.source = 'localhost';
    message.content = this.content;

    this.sent.push(message);
    this.webSocket.messages.next(message);
  }

  stopReceiving(){
    const popup = new Overlay({
      element: document.getElementById('popup') as HTMLElement,
    });

    this.map.addOverlay(popup);
    const element = popup.getElement();
    const marker = new Overlay({
      position: olProj.fromLonLat([19.835684294227466,45.25230882879536]),
      positioning: 'center-center',
      element: document.getElementById('marker') as HTMLElement,
      stopEvent: false,
    });
    this.map.addOverlay(marker);
  }
  
}