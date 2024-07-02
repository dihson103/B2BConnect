import { Event } from '@/types/event.types'
import { Table, TableBody, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import EventTableRow from '@/app/admin/events/components/table-row'

type Props = {
  data?: Event[]
}

export default function AppEventTable({ data }: Props) {
  return (
    <div className='rounded-md border mt-2'>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead className=' w-[100px] sm:table-cell'>
              <span className='sr-only'>Ảnh</span>
            </TableHead>
            <TableHead>Tên sự kiện</TableHead>
            <TableHead>Trạng thái sự kiện</TableHead>
            <TableHead>Địa điểm tổ chức</TableHead>
            <TableHead className='hidden md:table-cell'>Ngày bắt đầu</TableHead>
            <TableHead className='hidden md:table-cell'>Kết thúc</TableHead>
            <TableHead>
              <span className='sr-only'>Hành động</span>
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>{data && data.map((event) => <EventTableRow key={event.id} data={event} />)}</TableBody>
      </Table>
    </div>
  )
}
