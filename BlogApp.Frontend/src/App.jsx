import { useState } from 'react'
import './App.css'
import ApiCall from './ApiCall'

function App() {
  const [count, setCount] = useState(0)

  return (
    <div>
      <ApiCall/>
    </div>
  )
}

export default App
