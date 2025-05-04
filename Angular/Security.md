### Preventing cross-site scripting (XSS)
- angular treats all values as untrusted by default and escapes all untrusted values that are inserted into the DOM
#### Sanitation
- inspection of untrusted value and turn it into a value that is safe to insert into the DOM
- HTML - `innerHtml`, Style - `style` binding, URL - `<a href>`, Resource URL - `<script src>`
- angular sanitizes HTMLs and URLs but not resource URLs since they contain arbitrary code
- interpolated content is always escaped 
- use of DOM APIs and many third party APIs contain unsafe methods that are not automatically sanitized, if unavoidable, use `DomSanitizer`
#### Trusting safe values (`DomSanitizer`)
- angular provides a service (`DomSanitizer` from `@angular/platform-browser`) to bypass security checks for trusted values which exposes sanitation methods:
	- `bypassSecurityTrustHtml` - html
	- `bypassSecurityTrustStyle` - styles
	- `bypassSecurityTrustScript` - scripts
	- `bypassSecurityTrustUrl` - URL (`href` or `src`)
	- `bypassSecurityTrustResourceUrl` - `iframe` sources
- can implement a pipe to conveniently apply sanitation
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
#### Content security policy
- can be configured in your angular application
- or can be configured in the web server, default is same-origin policy:
```nginx
%% nginx server %%
set $cspNonce $request_id;
sub_filter_once off;
sub_filter_types *;
sub_filter randomNonceGoesHere $cspNonce;

add_header Content-Security-Policy "
	default-src 'self' ;
	script-src 'self' 'unsafe-inline' https://www.youtube.com 'nonce-$cspNonce';
	style-src 'self' https://fonts.googleapis.com 'nonce-$cspNonce';
	img-src 'self' data: https://www.youtube.com https://serif.com;
	font-src 'self' data: https://fonts.gstatic.com;
	frame-src 'self' https://www.youtube.com;
	connect-src 'self' ;
" always;
```
- `'unsafe-inline'` is not recommended as this essentially defeats the purpose of CSP, though for `style-src`, it is deemed ok
- it is recommended that the CSP is configured in the server instead so that the application does not need to be redeployed to generate unique nonces
- that way it will work is that the server will create the nonce and inject this into the application via a variable selector (in the above's case, `randomNonceGoesHere)
- the server should include a randomly-generated nonce in the HTTP header for each request. You must provide this nonce to Angular so that the framework can render `<style>` elements. You can set the nonce for Angular in one of two ways:
	1. Set the `ngCspNonce` attribute on the root application element as `<app ngCspNonce="randomNonceGoesHere"></app>`. Use this approach if you have access to server-side templating that can add the nonce both to the header and the `index.html` when constructing the response.
	2. Provide the nonce using the `CSP_NONCE` injection token. Use this approach if you have access to the nonce at runtime and you want to be able to cache the `index.html`.
```ts
import {bootstrapApplication, CSP_NONCE} from '@angular/core';
import {AppComponent} from './app/app.component';
bootstrapApplication(AppComponent, {
  providers: [{
    provide: CSP_NONCE,
    useValue: globalThis.myRandomNonceValue
  }]
});
```

```html
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Your App</title>
  <script nonce="$cspNonce">
    globalThis.randomNonceGoesHere = '$cspNonce';
  </script>
  <script src="main.js" nonce="$cspNonce"></script>
</head>
<body>
  <app-root></app-root>
</body>
</html>
```

### HTTP-level vulnerabilities
#### Cross-site request forgery (CSRF/XSRF)
- attacker uses cookie/authentication of a legit website (which might be saved in the user's browser) to send requests to the application's web server
- anti-XSRF pattern: server sends a cookie with authentication token, client code reads this and adds a custom request header with the token in all requests, server compares received cookie value to the request header value and tries to match
- all browsers implement same origin policy, only from code from the website on which cookies are set can read the cookies and set custom headers
#### Using `HttpClient` for XSRF security
- interceptor reads a token from a cookie (on either the page load or the first GET request), by default `XSRF-TOKEN` (set in the backend), and sets it as an HTTP header, `X-XSRF-TOKEN` for all mutating requests (POST), requests that can change the state on the backend
- to override the default token name:
```ts
export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(
      withXsrfConfiguration({
        cookieName: 'CUSTOM_XSRF_TOKEN',
        headerName: 'X-Custom-Xsrf-Header',
      }),
    ),
  ]
};
```
- to disable the XSRF protection:
```ts
export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(
      withNoXsrfProtection(),
    ),
  ]
};
```