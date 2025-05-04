#### Handle responsiveness in component
```ts
isLeftSidebarCollapsed = signal<boolean>(false);
screenWidth = signal<number>(window.innerWidth)

@HostListener('window:resize')
onResize() {
	this.screenWidth.set(window.innerWidth);
	if (this.screenWidth() < 768) {
		this.isLeftSidebarCollapsed.set(true)
	}
}
```

#### `APP_INITIALIZER`
- delay angular application bootstrap process until some asynchronous task are completed
- usually injected in the @NgModule
```ts
export function initializeApp(configService: ConfigService): () => Promise<any> {
  return (): Promise<any> => {
    return configService.loadConfig();
  };
}
```

```ts
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ConfigService } from './config.service';

function initializeApp(config: ConfigService) {
  return () => config.load();
}

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, HttpClientModule],
  providers: [
    ConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [ConfigService],
      multi: true // without this, only last registered initializer would run
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
```

```ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  constructor(private http: HttpClient) {}

  loadConfig(): Promise<any> {
    return this.http.get('/assets/config.json').toPromise();
  }
}
```