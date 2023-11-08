Reference: [Do This Instead Of Representing State With Booleans - YouTube](https://www.youtube.com/watch?v=4Lom_lqSGoY)
### Enums
```tsx
// TypeScript
type State= 'start' | 'playing' | 'paused' | 'gameover'

// TypeScript Enum
enum State {
	'START' = 'start',
	'PLAYING' = 'playing',
	'PAUSED' = 'paused',
	'GAME_OVER' = 'gameover'
}

// JavaScript
const state = {
	START: 'start',
	PLAYING: 'playing',
	PAUSED: 'paused',
	GAME_OVER: 'gameover'
}
```

### State Machines
```tsx
// ??
```

### Reducer Pattern
Becomes action driven
