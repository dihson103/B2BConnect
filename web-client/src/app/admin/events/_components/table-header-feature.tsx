import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { File, PlusCircle, Search } from 'lucide-react'
import { SearchEventOption } from '@/types/event.types'
import CreateEventForm from '@/app/admin/events/_components/create-event-form'

type Props = {
  searchParams: SearchEventOption
}

export default function EventTableHeader({ searchParams }: Props) {
  return (
    <div className='flex items-center'>
      <div className='flex items-center gap-2'>
        <div className='relative'>
          <Search className='absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground' />
          <Input
            type='search'
            placeholder='Search...'
            className='w-full rounded-lg bg-background pl-8 md:w-[200px] lg:w-[320px]'
          />
        </div>
        <Select>
          <SelectTrigger className='w-[180px]'>
            <SelectValue placeholder='Vai trò' />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value='1'>Chủ</SelectItem>
            <SelectItem value='2'>Quản lý điểm danh</SelectItem>
            <SelectItem value='3'>Quản lý đếm hàng</SelectItem>
          </SelectContent>
        </Select>
        <Select>
          <SelectTrigger className='w-[180px]'>
            <SelectValue placeholder='Trạng thái' />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value='true'>Đang làm</SelectItem>
            <SelectItem value='false'>Đã nghỉ</SelectItem>
          </SelectContent>
        </Select>
      </div>

      <div className='ml-auto flex items-center gap-2'>
        <Button size='sm' variant='outline' className='h-7 gap-1'>
          <File className='h-3.5 w-3.5' />
          <span className='sr-only sm:not-sr-only sm:whitespace-nowrap'>Export</span>
        </Button>
        <CreateEventForm />
      </div>
    </div>
  )
}
