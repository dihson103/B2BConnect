'use client'

import { SearchBusinessQueryOptionKeys } from '@/types/business.types'
import { ChangeEvent, useState } from 'react'

type props = {
  isChecked: boolean
  value: string
  id: string
  display: string
  handleCheckBoxChange: (name: SearchBusinessQueryOptionKeys, term: string, isChecked: boolean) => void
  searchName: SearchBusinessQueryOptionKeys
}

export default function SearchCheckBox({ isChecked, value, id, display, handleCheckBoxChange, searchName }: props) {
  const [checked, setChecked] = useState<boolean>(isChecked)

  const handleChangeValue = (event: ChangeEvent<HTMLInputElement>) => {
    setChecked(event.target.checked)
    handleCheckBoxChange(searchName, event.target.value, event.target.checked)
  }

  return (
    <div className='flex items-center space-x-2 mb-3'>
      <input
        type='checkbox'
        className='w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded dark:ring-offset-gray-800 dark:bg-gray-700 dark:border-gray-600'
        id={id}
        value={value}
        checked={checked}
        onChange={handleChangeValue}
      />
      <label
        htmlFor='terms'
        className='text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70'
      >
        {display}
      </label>
    </div>
  )
}
