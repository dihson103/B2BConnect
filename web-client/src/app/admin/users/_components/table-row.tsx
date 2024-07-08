import UserTableRowAction from '@/app/admin/users/_components/table-row-action'
import { Badge } from '@/components/ui/badge'
import { TableCell, TableRow } from '@/components/ui/table'
import { User } from '@/types/user.types'
import Image from 'next/image'

type Props = {
  data: User
}

export default function UserTableRow({ data }: Props) {
  return (
    <TableRow>
      <TableCell className='hidden sm:table-cell'>
        <Image
          alt='Product image'
          className='aspect-square rounded-md object-cover'
          height='30'
          src={'/sdfsd.png'}
          width='30'
        />
      </TableCell>
      <TableCell className='font-medium'>{data.id}</TableCell>
      <TableCell>{`${data.firstName} ${data.lastName}`}</TableCell>
      <TableCell>
        <Badge variant='outline'>{data.isActive ? 'Active' : 'NonActive'}</Badge>
      </TableCell>
      <TableCell className='hidden md:table-cell'>{data.phone}</TableCell>
      <TableCell className='hidden md:table-cell'>{data.address}</TableCell>
      <TableCell>
        <UserTableRowAction id={data.id} />
      </TableCell>
    </TableRow>
  )
}
