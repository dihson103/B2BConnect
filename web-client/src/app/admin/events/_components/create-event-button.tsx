'use client'

import { Button } from '@/components/ui/button'
import { PlusCircle } from 'lucide-react'
import { useRouter } from 'next/navigation'

export default function CreateEventButton() {
  const router = useRouter()

  const handlerClick = () => {
    router.push('/admin/events/create')
  }

  return (
    <Button size='sm' className='h-7 gap-1' onClick={handlerClick}>
      <PlusCircle className='h-3.5 w-3.5' />
      <span className='sr-only sm:not-sr-only sm:whitespace-nowrap'>Thêm sự kiện</span>
    </Button>
  )
}
