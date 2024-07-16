import DropdownMenuItemNavigate from '@/components/drop-down-menu-item-navigate'
import { Button } from '@/components/ui/button'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import { MoreHorizontal } from 'lucide-react'
import React from 'react'

export default function BusinessTableRowAction({ id }: { id: string }) {
  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button aria-haspopup='true' size='icon' variant='ghost'>
          <MoreHorizontal className='h-4 w-4' />
          <span className='sr-only'>Toggle menu</span>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align='end'>
        <DropdownMenuLabel>Hành động</DropdownMenuLabel>
        <DropdownMenuItemNavigate url={`/admin/businesses/${id}`} displayName='Xem chi tiết' />
        <DropdownMenuItemNavigate url={`/admin/businesses/update/${id}`} displayName='Chỉnh sửa' />
      </DropdownMenuContent>
    </DropdownMenu>
  )
}
