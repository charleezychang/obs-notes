### ngOnInit
- called after constructor (note that constructor is only called when it is loaded into the DOM)
- for initialization
- if a child and parent component is loaded into the DOM, the order of loading is: parent constructor > child constructor > parent ngOnInit > child ngOnInit

### ngOnDestroy
- called when the component is removed from the DOM
- cleaning activities i.e. setInterval -> clearInterval

### ngOnChanges
- called before ngOnInit (must be from @Input decorator)
- accepts a `SimpleChanges` object which has the input variables as properties which have further properties of `currentValue`, `firstChange` (boolean), and `previousValue`

### ngDoCheck