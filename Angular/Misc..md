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
#### Circumventing `[innerHTML]` security risks (`DomSanitizer`)
- By default, angular escapes any text that are passed into the template due to the its built-in code injection defenses, also known as XSS defenses or Cross-Site Scripting defenses
- Framework provides a service (`DomSanitizer` from `@angular/platform-browser`) to bypass security checks for trusted values which exposes sanitation methods:
	- `bypassSecurityTrustHtml` - html
	- `bypassSecurityTrustStyle` - styles
	- `bypassSecurityTrustScript` - scripts
	- `bypassSecurityTrustUrl` - URL
	- `bypassSecurityTrustResourceUrl` - iframe sources
- Can implement a pipe to conveniently apply sanitation
```ts
@Pipe({
  name: "safeHtml",
  standalone: true,
})
export class SafeHtmlPipe {
  constructor(private sanitizer: DomSanitizer) {}

  transform(html) {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }
}
```

```ts
@Component({
  standalone: true,
  imports: [SafeHtmlPipe],
  template: ` 
  <div [innerHTML]="someHtmlContent | safeHtml">
  </div> `,
})
export class TestComponent {}
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

#### NgModules