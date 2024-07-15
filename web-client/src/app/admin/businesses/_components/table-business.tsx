import { Table, TableBody, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import { BusinessResponse } from '@/types/business.types'
import BusinessTableRow from '@/app/admin/businesses/_components/table-row'

type Props = {
  data?: BusinessResponse[]
}

export default function AppBusinessTable({ data }: Props) {
  return (
    <div className='rounded-md border mt-2'>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead className=' w-[100px] sm:table-cell'>
              <span className='sr-only'>Ảnh</span>
            </TableHead>
            <TableHead>Mã số thuế</TableHead>
            <TableHead>Tên doanh nghiệp</TableHead>
            <TableHead className='hidden md:table-cell'>Quy mô doanh nghiệp</TableHead>
            <TableHead>
              <span className='sr-only'>Hành động</span>
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>{data && data.map((business) => <BusinessTableRow key={business.id} data={business} />)}</TableBody>
      </Table>
    </div>
  )
}
