[4 Runtime Performance Optimizations](https://www.youtube.com/watch?v=f8sA-i6gkGQ)
#### Zone Pollution
- occurs when the Angular zone wraps callbacks that trigger redundant change detection cycles
- identification: use Angular's profiler
- resolution: move outside the Angular zone
```ts
import { Component, NgZone, OnInit } from '@angular/core';
@Component(...)
class AppComponent implements OnInit {
  constructor(private ngZone: NgZone) {}
  ngOnInit() {
    this.ngZone.runOutsideAngular(() => setInterval(pollForUpdates), 500);
  }
}
```
- in some cases, 3rd party libraries can cause this, able to move outside the zone and re-enter
```ts
import { Component, NgZone, OnInit, output } from '@angular/core';
import * as Plotly from 'plotly.js-dist-min';
@Component(...)
class AppComponent implements OnInit {
  plotlyClick = output<Plotly.PlotMouseEvent>();
  constructor(private ngZone: NgZone) {}
  ngOnInit() {
    this.ngZone.runOutsideAngular(() => {
      this.createPlotly();
    });
  }
  private async createPlotly() {
    const plotly = await Plotly.newPlot('chart', data);
    plotly.on('plotly_click', (event: Plotly.PlotMouseEvent) => {
      this.ngZone.run(() => {
        this.plotlyClick.emit(event);
      });
    });
  }
}
```
#### Out of bounds
- occurs when a component is used multiple times in a page and then an event triggers all of them to re-trigger change detection
- identification: in the profiler, find components that are not supposed to be affected by a particular interaction
- resolution: use `OnPush` and considering refactoring
#### Recalculation of referentially transparent expressions
- referentially transparent - where a function, given the same inputs, will always produce the same output without causing any side effects
- do not need to calculate the expressions between CD cycles
- identification: in the profiler, detection for changes takes longer than expected given the state changes
- resolution: use pure pipes or memoization
#### Large component trees
- in a large array, if you push a new value, it will cause the whole array to re-render
- identification: a component with large view takes too long
- resolution: on demand render components (virtual lists, pagination)
