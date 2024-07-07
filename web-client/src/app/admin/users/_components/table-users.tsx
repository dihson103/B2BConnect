import UserTableRow from '@/app/admin/users/_components/table-row'
import { Table, TableBody, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import { User } from '@/types/user.types'

type Props = {
  data?: User[]
}

export default function AppUserTable({ data }: Props) {
  return (
    <div className='rounded-md border mt-2'>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead className=' w-[100px] sm:table-cell'>
              <span className='sr-only'>Ảnh</span>
            </TableHead>
            <TableHead>Cccd</TableHead>
            <TableHead>Họ và tên</TableHead>
            <TableHead>Trạng thái</TableHead>
            <TableHead className='hidden md:table-cell'>Số điện thoại</TableHead>
            <TableHead className='hidden md:table-cell'>Địa chỉ</TableHead>
            <TableHead>
              <span className='sr-only'>Hành động</span>
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>{data && data.map((user) => <UserTableRow key={user.id} data={user} />)}</TableBody>
      </Table>
    </div>
  )
}
