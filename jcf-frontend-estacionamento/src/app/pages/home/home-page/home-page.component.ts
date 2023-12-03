import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { Apiconfig } from "src/app/Utils/apiconfig";
import { ConnectionService } from "src/app/services/connection/connection.service";

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html'
  })
  export class HomePageComponent implements OnInit, OnDestroy {
    isConnected = false;
    private connectionSubscription: Subscription;
    apiUrl: string;
    
    constructor(private connectionService: ConnectionService) {}

  ngOnInit() {
    this.apiUrl = Apiconfig.getApiUrl();

    this.connectionSubscription = this.connectionService
      .testConnectionPeriodically(5) 
      .subscribe(isConnected => {
        this.isConnected = isConnected;
      });
  }

  ngOnDestroy() {
    this.connectionSubscription.unsubscribe();
  }
    
  }
  