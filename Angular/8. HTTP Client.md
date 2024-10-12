- Angular provides a client HTTP API (`HttpClient`) to communicate with a server
- returns RxJS Observable which when subscribed, sends the request and emits the results when the server responds (every subscription will send a new request)
- takes 2 arguments: string endpoint URL and optional options object
##### `get()`
- `HttpClient` assumes servers will return JSON data, other response types can be expected by using `responseType`: (`json` (default), `string`, `arraybuffer`, `blob`)
- Option: `params` can be used to insert query parameters into the URL, pass a literal object or alternatively, pass an instance of `HttpParams` (Note: this is immutable, use `set()` and `append()`)
```ts
const baseParams = new HttpParams().set('filter', 'all');
// overwrites the param 'filter'
baseParams.set('filter', 'none')
// appends value 'single' to key 'filter'
// filter now has values 'none' and 'single', ?filter=none&filter=single
baseParams.append('filter', 'single')
http.get('/api/config', {
  // adds another param with key 'details'
  params: baseParams.set('details', 'enabled'),
}).subscribe(config => {
  // ...
});
```
- Option: `headers` pass a literal object or an instance of `HttpHeaders`, similar to `HttpParams`
- Option: `response` to examine the whole response (header and body)
##### `post()`
- accepts an additional `body` argument before options parameter which are serialized (transformed into suitable format for transmission over the network)
- important to `.subscribe()` to actually fire the request
- Option: `reportProgress` and `observe`, report the lifecycle of a request. Disabled by default as they have a performance cost. 
```ts
http.post('/api/upload', myData, {
  reportProgress: true,
  observe: 'events',
}).subscribe(event => {
  switch (event.type) {
    case HttpEventType.UploadProgress:
      console.log('Uploaded ' + event.loaded + ' out of ' + event.total + ' bytes');
      break;
    case HttpEventType.Response:
      console.log('Finished uploading!');
      break;
    // Other HttpEventType: Sent, ResponseHeader, DownloadProgress, User
  }
});
```
#### Handling request failure
- two ways request can fail: (1) network error preventing request from reaching the server, (2) backend receives request but fails to process it and returns an error response
- these are both captured in an `HttpErrorReponse`, which is returned through the `Observable`'s error channel (second parameter)
- could use `RxJS`'s `catchError` operator to transform an error response into a value for the UI