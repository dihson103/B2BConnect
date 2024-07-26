import { getIndustriesAction } from '@/actions/industry.Actions'
import SearchBusinessInput from '@/app/(user_pages)/home/_components/search-input'
import BusinessItem from '@/components/business-item'
import AppPagination from '@/components/table-pagination'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { SearchBusinessQueryOption } from '@/types/business.types'
import { IndustryResponse } from '@/types/industry.types'

type props = {
  searchParams: SearchBusinessQueryOption
}

export default async function HomePage({ searchParams }: props) {
  let industries: IndustryResponse[] = []

  try {
    const industriyResponse = await getIndustriesAction('')
    industries = industriyResponse.data ?? []
  } catch (error) {}

  return (
    <div>
      <h1 className='text-4xl font-bold uppercase mb-5 text-center'>Tìm kiếm doanh nghiệp</h1>
      <div className='grid gap-4 md:grid-cols-[200px_1fr] lg:grid-cols-[1fr_2fr] lg:gap-8'>
        <div className='grid auto-rows-max items-start gap-4'>
          <Card x-chunk='dashboard-07-chunk-0'>
            <CardHeader>
              <CardTitle className='text-lg'>Tìm kiếm doanh nghiệp</CardTitle>
            </CardHeader>
            <CardContent>
              <SearchBusinessInput searchOptions={searchParams} industries={industries} />
            </CardContent>
          </Card>
        </div>
        <div className='grid auto-rows-max items-start gap-4 lg:gap-8'>
          <BusinessItem />
          <BusinessItem />
          <BusinessItem />
          <BusinessItem />
          <BusinessItem />
          <BusinessItem />
          <BusinessItem />
          <BusinessItem />
          <BusinessItem />
        </div>
      </div>
      <div className='mt-5'>
        <AppPagination currentPage={1} totalPages={10} />
      </div>
    </div>
  )
}
