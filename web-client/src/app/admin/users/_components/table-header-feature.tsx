'use client'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { SearchUserOption } from '@/types/user.types'
import { File, PlusCircle, Search } from 'lucide-react'
import { ChangeEvent, useState } from 'react'
import { usePathname, useRouter, useSearchParams } from 'next/navigation'

type Props = {
  searchOptions: SearchUserOption
}

export default function UserTableHeader({ searchOptions }: Props) {
  const searchParams = useSearchParams()
  const pathname = usePathname()
  const router = useRouter()

  const [isActive, setIsActive] = useState<string>(searchOptions.isActive == 'false' ? 'false' : 'true')
  const [roleId, setRoleId] = useState(searchOptions.roleId ? searchOptions.roleId.toString() : '')
  const [searchTearm, setSearchTearm] = useState(searchOptions.searchTearm ?? '')

  const isActiveChange = (value: string) => {
    setIsActive(value)
    handleSearch('isActive', value)()
  }

  const roleIdChange = (value: string) => {
    setRoleId(value)
    handleSearch('roleId', value)()
  }

  const searchTearmChange = (event: ChangeEvent<HTMLInputElement>) => {
    setSearchTearm(event.target.value)
    handleSearch('searchTearm', event.target.value)()
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
            placeholder='Search...'
            defaultValue={searchTearm}
            onChange={searchTearmChange}
            className='w-full rounded-lg bg-background pl-8 md:w-[200px] lg:w-[320px]'
          />
        </div>
        <Select value={roleId} onValueChange={(value) => roleIdChange(value)}>
          <SelectTrigger className='w-[180px]'>
            <SelectValue placeholder='Vai trò' />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value='1'>Chủ</SelectItem>
            <SelectItem value='2'>Quản lý điểm danh</SelectItem>
            <SelectItem value='3'>Quản lý đếm hàng</SelectItem>
          </SelectContent>
        </Select>
        <Select value={isActive} onValueChange={(value) => isActiveChange(value)}>
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
        <Button size='sm' className='h-7 gap-1'>
          <PlusCircle className='h-3.5 w-3.5' />
          <span className='sr-only sm:not-sr-only sm:whitespace-nowrap'>Add User</span>
        </Button>
      </div>
    </div>
  )
}
