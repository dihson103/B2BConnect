'use client'

import { Input } from '@/components/ui/input'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { File, Search } from 'lucide-react'
import { SearchEventOption } from '@/types/event.types'
import { usePathname, useRouter, useSearchParams } from 'next/navigation'
import { ChangeEvent, useState } from 'react'
import CreateEventButton from '@/app/admin/events/_components/create-event-button'

type Props = {
  searchOptions: SearchEventOption
}

export default function EventTableHeader({ searchOptions }: Props) {
  const searchParams = useSearchParams()
  const pathname = usePathname()
  const router = useRouter()

  const [searchTearm, setSearchTearm] = useState(searchOptions.searchTearm ?? '')
  const [status, setStatus] = useState<number>(searchOptions.status ?? 0)

  const searchTearmChange = (event: ChangeEvent<HTMLInputElement>) => {
    setSearchTearm(event.target.value)
    handleSearch('searchTearm', event.target.value)()
  }

  const statusChange = (value: string) => {
    setStatus(Number(value))
    handleSearch('status', value)()
  }

  const handleSearch = (name: string, term: string) => () => {
    const params = new URLSearchParams(searchParams)
    if (term) {
      params.set(name, term)
    } else {
      params.delete(name)
    }
    router.push(`${pathname}?${params.toString()}`, { scroll: false })
  }

  return (
    <div className='flex items-center'>
      <div className='flex items-center gap-2'>
        <div className='relative'>
          <Search className='absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground' />
          <Input
            type='search'
            defaultValue={searchTearm}
            onChange={searchTearmChange}
            placeholder='Tìm kiếm sự kiện'
            className='w-full rounded-lg bg-background pl-8 md:w-[200px] lg:w-[320px]'
          />
        </div>
        <Select value={status.toString()} onValueChange={(value) => statusChange(value)}>
          <SelectTrigger className='w-[180px]'>
            <SelectValue placeholder='Trạng thái sự kiện' />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value='0'>Sắp diễn ra</SelectItem>
            <SelectItem value='1'>Đang diễn ra</SelectItem>
            <SelectItem value='2'>Đã kết thúc</SelectItem>
            <SelectItem value='3'>Đã bị hủy</SelectItem>
          </SelectContent>
        </Select>
      </div>

      <div className='ml-auto flex items-center gap-2'>
        {/* <Button size='sm' variant='outline' className='h-7 gap-1'>
          <File className='h-3.5 w-3.5' />
          <span className='sr-only sm:not-sr-only sm:whitespace-nowrap'>Export</span>
        </Button> */}
        <CreateEventButton />
      </div>
    </div>
  )
}
